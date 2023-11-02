using System.Collections.Generic;
using System.Linq;
using Pulumi;
using Azure = Pulumi.Azure;

return await Deployment.RunAsync(() => 
{
    var config = new Config();
    var resourceGroupName = config.Get("resourceGroupName") ?? "dom-blog-pulumi";
    var location = config.Get("location") ?? "westeurope";
    var storageAccountName = config.Get("storageAccountName") ?? "domblogstorage";
    var blobContainerName = config.Get("blobContainerName") ?? "bookcovers";
    var dbServerName = config.Get("dbServerName") ?? "domblog-postgres-db";
    var dbUsername = config.Get("dbUsername") ?? "sAIx3L1wQcdp56";
    var dbPassword = config.Get("dbPassword") ?? "SNaNgy0smCQfkO";
    var dbDatabaseName = config.Get("dbDatabaseName") ?? "blog";
    var appName = config.Get("appName") ?? "dom-blog";
    var appDnsName = config.Get("appDnsName") ?? "dom-blog.azurewebsites.net";
    var dotnetAppDllName = config.Get("dotnetAppDllName") ?? "Domgie.Client.dll";
    var rg = new Azure.Core.ResourceGroup("rg", new()
    {
        Location = location,
        Name = resourceGroupName,
    });

    //###########################################################################
    // Network Configuration
    var nsg = new Azure.Network.NetworkSecurityGroup("nsg", new()
    {
        Location = location,
        Name = $"{appName}-nsg",
        ResourceGroupName = resourceGroupName,
    });

    var vnet = new Azure.Network.VirtualNetwork("vnet", new()
    {
        AddressSpaces = new[]
        {
            "172.16.0.0/16",
        },
        Location = location,
        Name = $"{appName}-vnet",
        ResourceGroupName = resourceGroupName,
    });

    //###########################################################################
    // Storage Account & Blob Container
    var appStorageAccount = new Azure.Storage.Account("appStorageAccount", new()
    {
        AccountReplicationType = "LRS",
        AccountTier = "Standard",
        Location = location,
        Name = storageAccountName,
        ResourceGroupName = resourceGroupName,
    });

    var appBlobContainer = new Azure.Storage.Container("appBlobContainer", new()
    {
        ContainerAccessType = "blob",
        Name = blobContainerName,
        StorageAccountName = storageAccountName,
    });

    //###########################################################################
    // PostgreSQL Server
    var postgresSubnet = new Azure.Network.Subnet("postgresSubnet", new()
    {
        AddressPrefix = "172.16.0.0/24",
        Name = "postgresql",
        ResourceGroupName = resourceGroupName,
        VirtualNetworkName = vnet.Name,
        ServiceEndpoints = new[]
        {
            "Microsoft.Storage",
        },
        Delegations = new[]
        {
            new Azure.Network.Inputs.SubnetDelegationArgs
            {
                Name = "dlg-Microsoft.DBforPostgreSQL-flexibleServers",
                ServiceDelegation = new Azure.Network.Inputs.SubnetDelegationServiceDelegationArgs
                {
                    Actions = new[]
                    {
                        "Microsoft.Network/virtualNetworks/subnets/join/action",
                    },
                    Name = "Microsoft.DBforPostgreSQL/flexibleServers",
                },
            },
        },
    });

    var postgresDnsZone = new Azure.PrivateDns.Zone("postgresDnsZone", new()
    {
        Name = $"{dbServerName}.private.postgres.database.azure.com",
        ResourceGroupName = resourceGroupName,
    });

    var postgresDnsARecord = new Azure.PrivateDns.ARecord("postgresDnsARecord", new()
    {
        Name = "c9102632dae7",
        Records = new[]
        {
            "172.16.0.4",
        },
        ResourceGroupName = resourceGroupName,
        Ttl = 30,
        ZoneName = postgresDnsZone.Name,
    });

    var postgresVnetLink = new Azure.PrivateDns.ZoneVirtualNetworkLink("postgresVnetLink", new()
    {
        Name = "yp5nu4pqnrweg",
        PrivateDnsZoneName = postgresDnsZone.Name,
        ResourceGroupName = resourceGroupName,
        VirtualNetworkId = vnet.Id,
    });

    var postgresServer = new Azure.PostgreSql.Server("postgresServer", new()
    {
        Location = location,
        Name = dbServerName,
        ResourceGroupName = resourceGroupName,
        AdministratorLogin = dbUsername,
        AdministratorLoginPassword = dbPassword,
        SkuName = "GP_Standard_D4s_v3",
        Version = "14",
        StorageProfile = new Azure.PostgreSql.Inputs.ServerStorageProfileArgs
        {
            BackupRetentionDays = 7,
            GeoRedundantBackup = "Disabled",
            StorageMb = 32768,
        },
    });

    var postgresDatabase = new Azure.PostgreSql.Database("postgresDatabase", new()
    {
        Name = dbDatabaseName,
        ServerName = postgresServer.Name,
    });

    var postgresVnetAssoc = new Azure.Network.SubnetNetworkSecurityGroupAssociation("postgresVnetAssoc", new()
    {
        NetworkSecurityGroupId = nsg.Id,
        SubnetId = postgresSubnet.Id,
    });

    //###########################################################################
    // App
    var appSubnet = new Azure.Network.Subnet("appSubnet", new()
    {
        Name = appName,
        ResourceGroupName = resourceGroupName,
        VirtualNetworkName = vnet.Name,
        AddressPrefix = "172.16.1.0/24",
        ServiceEndpoints = new[]
        {
            "Microsoft.Storage",
        },
        Delegations = new[]
        {
            new Azure.Network.Inputs.SubnetDelegationArgs
            {
                Name = "delegation",
                ServiceDelegation = new Azure.Network.Inputs.SubnetDelegationServiceDelegationArgs
                {
                    Actions = new[]
                    {
                        "Microsoft.Network/virtualNetworks/subnets/action",
                    },
                    Name = "Microsoft.Web/serverFarms",
                },
            },
        },
    });

    var appServicePlan = new Azure.AppService.Plan("appServicePlan", new()
    {
        Location = location,
        Name = $"{appName}-plan",
        Kind = "Linux",
        ResourceGroupName = resourceGroupName,
        Sku = new Azure.AppService.Inputs.PlanSkuArgs
        {
            Size = "B1",
            Tier = "Basic",
        },
    });

    var appSubnetAssc = new Azure.Network.SubnetNetworkSecurityGroupAssociation("appSubnetAssc", new()
    {
        NetworkSecurityGroupId = nsg.Id,
        SubnetId = appSubnet.Id,
    });

    var appService = new Azure.AppService.AppService("appService", new()
    {
        Location = location,
        Name = appName,
        ResourceGroupName = resourceGroupName,
        AppServicePlanId = appServicePlan.Id,
        Logs = new Azure.AppService.Inputs.AppServiceLogsArgs
        {
            HttpLogs = new Azure.AppService.Inputs.AppServiceLogsHttpLogsArgs
            {
                FileSystem = new Azure.AppService.Inputs.AppServiceLogsHttpLogsFileSystemArgs
                {
                    RetentionInDays = 0,
                    RetentionInMb = 35,
                },
            },
        },
        SiteConfig = new Azure.AppService.Inputs.AppServiceSiteConfigArgs
        {
            AlwaysOn = false,
            FtpsState = "AllAllowed",
            AppCommandLine = $"dotnet {dotnetAppDllName}",
            DotnetFrameworkVersion = "v7.0",
        },
        ConnectionStrings = new[]
        {
            new Azure.AppService.Inputs.AppServiceConnectionStringArgs
            {
                Name = "PostgreSQL",
                Type = "Custom",
                Value = postgresServer.Name.Apply(name => $"Server={name}.postgres.database.azure.com;Database={dbDatabaseName};Port=5432;User Id={dbUsername};Password={dbPassword};"),
            },
        },
        AppSettings = 
        {
            { "BlobStorage__ConnectionString", appStorageAccount.PrimaryConnectionString },
            { "BlobStorage__ContainerName", blobContainerName },
        },
    });

});

