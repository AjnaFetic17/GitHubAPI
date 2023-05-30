

# GitHub data manipulation

This project uses API call to retrieve data from the user for with the token has been issued for. For this app to work, you would need to have Twitter developer account and your own ApiKey and ApiSecret (if you would like to have Twitter Authentication) as well as Personal Access Token from GitHub. Since these credenials are confidential, secrets.json file was used. For production, Azure Key Vault is advised.

## Has the following structure:

- controllers (logic for each route)
- models - consists of helpers (helper methods and model helpers such as constants) and the C# files which represent Models in MVC arhitecture 
- services - contains interfaces and implementations of the interfaces (used to make a request to GitHub REST API and to cache the response so that the app does not need to send request for each API call)

## Prerequisites:

- .net 6

## Installing / Getting Started:

- download and install .net 6
- open Package Manager Console and type "update-database" so that the initial migrations are applied and we can use Identity for Authentication

### Development (locally):

If using Visual Studio Code, use the following commands to build and run the project:

### `dotnet build`

### `dotnet run`
After the application has started, navigate to provided hosting link (port starts with 7) and add /swagger

If using Visual Studio, use Visual Studio 2022 and simply start the project
# Steps to deploy an application to Microsoft Azure:
- Open the application in Visual Studio
- Right click on Project and click „Publish“
- Select a publish target to “Azure”
- Set Specific target to „Azure App Service“
- After selecting an Azure account in the top right corner, create a new Azure service by clicking on „+“ icon next to „App Service instances“
- New window will pop up. Set preferred „Name“; set Subscription name as „Azure subscription 1“; Create new „Resource group“ and a new „Hosting Plan“ (set „Size“ to Free)
- Click „Create“
- Click „Next“
- Choose „Publish“ and press „Finish“ 
- After everything is ready, press „Publish“ to deploy the application to Azure 
- In the hosting section you can find site url

To update deployed application, after some code changes, simply right click on the Project and press „Publish“. New Window will pop up. At the top there will be another „Publish“ button to press to update deployed application.
