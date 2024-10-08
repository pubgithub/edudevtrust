# edudevtrust
Software Developer Technical Assessment

### Follow the instructions to run the program. If you encounter any issues or if the setup is not working as expected, please don’t hesitate to contact me for assistance.
    email: pubuduec@gmail.com
    mobile: 07424552534

### Instructions: 

01. Set up the environment:
        VS Code Latest
        https://code.visualstudio.com/Download

        .NET 8.0 - .NET SDK x64
        https://dotnet.microsoft.com/en-us/download


02. VS Code Extentions:
        C# - Base language support for C# - Micrososft 
            v2.39.29
            Note: There is a bug in the latest C# extension. Please download version v2.39.29
            Bug: https://github.com/dotnet/vscode-csharp/issues/7533
        C# Dev Kit - Official C# extension from Microsoft.
            Latest Version

        REST Client - Nice to have

03. Install dependencies:
        <PackageReference Include="Microsoft.AspNetCore.Mvc.Testing" Version="8.0.8" />
        <PackageReference Include="Microsoft.AspNetCore.OData" Version="9.0.0" />
        <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="8.0.8" />
        <PackageReference Include="Microsoft.AspNetCore.TestHost" Version="8.0.8" />
        <PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="8.0.8" />
        <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.11.1" />
        <PackageReference Include="Moq" Version="4.20.72" />
        <PackageReference Include="Newtonsoft.Json" Version="13.0.3" />
        <PackageReference Include="Swashbuckle.AspNetCore" Version="6.4.0" />
        <PackageReference Include="xunit" Version="2.9.2" />

        Example:
        Go to VS Code terminal and run the command.
            dotnet add package Microsoft.AspNetCore.TestHost 
            dotnet add package Newtonsoft.Json

04. Run the program.
    Remove bin and obj folders.

    Run following command in the terminal. This will add swagger API Client.
        dotnet add package Swashbuckle.AspNetCore

    Go to VS Code terminal and run following command.
        dotnet restore
        dotnet clean
        dotnet build
        
    Click on the Program.cs file

    Click on "Run or Debug" Button - This will help to automatically browse the API with Swager UI which is an built in API testing client.

05. Tests
        File name : BookApiEndpointsTests.cs
        Note:
            Please note that test class is wriiten in the same project. But unable to run it.
            
            Message:
            A total of 1 test files matched the specified pattern. No test is available in C:\Users\pubud\OneDrive\Documents\projects\interview-assignments\edu-dev-trust-webapi\bin\Debug\net8.0\edu-dev-trust-webapi.dll. Make sure that test discoverer & executors are registered and platform & framework version settings are appropriate and try again.

            Additionally, path to test adapters can be specified using /TestAdapterPath command. Example  /TestAdapterPath:<pathToCustomAdapters>.

