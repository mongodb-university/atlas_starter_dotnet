# MongoDB Atlas & C# Sample Project

This repository contains a very basic example application 
built with the C# MongoDB Driver that connects to MongoDB 
Atlas. You can use this console application as a starting 
point and reference when building your C# applications.

## Prerequisites

To build and run this project, you will need Visual Studio
2019 Community Edition for MacOS or Windows, which you can 
download here:
[VS Community 2019 Downloads](https://visualstudio.microsoft.com/downloads/).

It is also helpful, but not necessary, to have a working 
installation of [Git](https://git-scm.com/downloads) version 
control.

## Getting Started

The following instructions explain how to get this project
connected to your instance of MongoDB Atlas.

### 1. Download the Repository

To get started with this sample project, download this repository to your
programming environment. You can close this project using Git
version control:

```
git clone git@github.com:mongodb-university/atlas_starter_dotnet.git
```

Or you can download the ZIP archive using your browser. If you download
this project as a ZIP archive, 
[unzip the archive](https://www.wikihow.com/Unzip-a-File) before proceeding.

### 2. Open the Project

1. In Visual Studio, select `File > Open... `

2. Navigate to the directory containing this project, and then the AtlasStarter
   folder.

3. Select the AtlasStarter.sln file, and then click `OK`.

### 4. Configure your Atlas Credentials

1. Open the  `Program.cs` file.

2. On line 17, replace the placeholder text with the connection string 
   to your Atlas cluster. For more information on finding the connection 
   string, see []().

```csharp
    var mongoUri = "<Your Atlas Connection String>";
```

### 5. Run the Project

1. Click the Run icon, or from the Run menu, choose **Start Debugging**.

Assuming you have the correct connection string, you have now connected 
the C# app to your MongoDB Atlas datastore.
Have fun modifying the code to experiment with the C# driver and MongoDB.

## Troubleshooting

Are you having trouble getting connected to your MongoDB Atlas instance? Double-check the following:

1. Have you replaced the `mongoUri` variable with a valid connection string provided by the Atlas UI?

2. Have you [whitelisted your current IP address](https://docs.atlas.mongodb.com/security-whitelist/) in the Atlas UI?
