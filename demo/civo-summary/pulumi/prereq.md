```bash

## Install
# Install CLI
curl -fsSL https://get.pulumi.com | sh

# Reload shell
zsh

# 'Configure Pulumi'
az login

# Set correct subscription
az account set -s b247b3ce-7fc4-4f48-9414-c62c97950e99


## Create new Project
mkdir quick-start-demo && cd quick-start-demo
pulumi new azure-csharp
# You will need access token: $PULUMI_ACCESS_KEY_CIVO_DEMO
# Switch to .NET 7 in .csproj
```
