```bash

## Install
# Install CLI
curl -fsSL https://get.pulumi.com | sh

# Reload shell
zsh

# 'Configure Pulumi'
az login

# Set correct subscription
az account set -s 70cfb438-fdb0-4032-9386-d4c7ab1f2c34


## Create new Project
mkdir quick-start-demo && cd quick-start-demo
pulumi new azure-csharp
# You will need access token: $PULUMI_ACCESS_KEY_CIVO_DEMO
# Switch to .NET 7 in .csproj
```
