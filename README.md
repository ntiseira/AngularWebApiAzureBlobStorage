# AngularWebApiAzureBlobStorage
Angular sample how upload file to blob storage azure,  with web api 


Transfer objects to  Azure Blob storage using .NET

This sample demonstrates a angular app  uploading  file to azure blob storage


About this sample

Scenario


1-The angular app upload the file through the UI, and then call to the orders api service.  
2-The web api, receive the file and then upload to azure blob storage.  

![alt text](https://github.com/ntiseira/AngularWebApiAzureBlobStorage/blob/master/ReadmeFiles/blobStorage.png)



Angular App

![alt text](https://github.com/ntiseira/AngularWebApiAzureBlobStorage/blob/master/ReadmeFiles/fe.jpg)

Web api


![alt text](https://github.com/ntiseira/AngularWebApiAzureBlobStorage/blob/master/ReadmeFiles/be.jpg)




How to run this sample To run this sample, you'll need:

-Visual Studio 2017
-Azure subscription, and blob storage created.
-NodeJs
-An Internet connection


If you don't have an Azure subscription, create a free account before you begin.


Create a storage account using the Azure portal
First, create a new general-purpose storage account to use for this quickstart.
  
Go to the Azure portal and log in using your Azure account.  
On the Hub menu, select New > Storage > Storage account - blob, file, table, queue.    

Enter a unique name for your storage account. Keep these rules in mind for naming your storage account:   

The name must be between 3 and 24 characters in length.  
The name may contain numbers and lowercase letters only.  

Make sure that the following default values are set:  

Deployment model is set to Resource manager.   
Account kind is set to General purpose.  
Performance is set to Standard.  
Replication is set to Locally Redundant storage (LRS).  
Select your subscription.  
For Resource group, create a new one and give it a unique name.  
Select the Location to use for your storage account.  
Check Pin to dashboard and click Create to create your storage account.  
After your storage account is created, it is pinned to the dashboard. Click on it to open it. Under Settings, click Access keys. Select the primary key and copy the associated Connection string to the clipboard, then paste it into a text editor for later use.  
Create a blob container, and save the name of it.

Run the sample

Web api

Clean the solution, rebuild the solution, and run it. You might want to go into the solution properties and set both projects as startup projects, with the service project starting first.

Put your storage account key in  key CloudBlobConnectionString on web config, and put replace the key ContainterName with your name of blob container.  

React app

-Open windows command line, or node console
-cd FE\ 
-npm install 
-npm start 

Community Help and Support Use Stack Overflow to get support from the community. Ask your questions on Stack Overflow first and browse existing issues to see if someone has asked your question before.  

If you find a bug in the sample, please raise the issue on GitHub Issues.

Contributing

If you wish to make code changes to samples, or contribute something new, please follow the GitHub Forks / Pull requests model: Fork the sample repo, make the change and propose it back by submitting a pull request.
 
 

 
 





