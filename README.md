# Sample NDEF Parser API App
[![Deploy to Azure](http://azuredeploy.net/deploybutton.png)](https://azuredeploy.net/)

## Deploying ##
Click the "Deploy to Azure" button above.  You can create new resources or reference existing ones (resource group, gateway, service plan, etc.)  **Site Name and Gateway must be unique URL hostnames.**  The deployment script will deploy the following:
 * Resource Group (optional)
 * Service Plan (if you don't reference exisiting one)
 * Gateway (if you don't reference existing one)
 * API App (NdefParser)
 * API App Host (this is the site behind the api app that this github code deploys to)

## API Documentation ##
The app has one action (Read vCard from NDEF) that parses out a partial set of data from a vCard record embedded in the passed NDEF message.

### Read vCard from NDEF Action ###
The action has the following inputs

| Input | Description |
| ----- | ----- |
| Base64 NDEF Message | Base64 encoded NDEF message which contains a vCard record as the first record |

The action will return the following output provided valid data
| Property | Friendly Name | Description |
| ----- | ----- | ----- |
| GivenName | Given Name | The given name contained in the vCard data |
| FamilyName | Family Name | The family name contained in the vCard data |
| EmailAddress | Email Address | The first email address contained in the vCard data |

