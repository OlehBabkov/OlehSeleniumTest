# Selenium Test Automation Project

## Overview
This project demonstrates a Selenium-based automation framework to interact with a web application, simulate user actions, and validate results. The project is built using **.NET Core** and leverages **Selenium WebDriver** to automate the browser.

### Two Approaches
There are two main approaches in this project:

Without ```bash testScript.json:``` Found in the master branch (```git "main try catch" commit```). This approach uses hardcoded steps and manually triggers commands.

With ```bash testScript.json:``` Found in the ```git "new version with json script" branch.``` This approach dynamically loads commands from a JSON file, making it easier to extend and modify test steps.

**Note:** While it's usually a better practice to split different approaches into separate branches, due to project constraints, both approaches are available in different commits.

## Implemented Steps

Below is a breakdown of the commands implemented and their purpose:

1. **openUrl**
- Description: This command opens a given URL in the browser.
- Usage:
    openUrl: Opens the URL specified in the testScript.json file.
- Parameters:
    url: The URL to open (e.g., http://example.com).

```json
{
  "command": "openUrl",
  "parameters": {
    "url": "https://demoqa.com/webtables"
  }
}
```

2. **removeAds**
- Description: This command removes ads (if any) from the page to ensure that the test area remains uncluttered.
- Usage:
    removeAds: Trigger the hiding of ads on the page.
- Parameters: None.

```json
{
  "command": "removeAds",
  "parameters": {}
}
```

3. **clickAddNewRecordButton**
- Description: This command clicks the "Add New Record" button to open the form for creating a new user.
- Usage:
    clickAddNewRecordButton: Clicks the button to open the form.
- Parameters: None.

```json
{
  "command": "clickAddNewRecordButton",
  "parameters": {}
}
```

4. **fillUserForm**
- Description: This command fills in the user details in the form.
- Usage:
    fillUserForm: Populates the form with the user data from userData.json (such as First Name, Last Name, Email, etc.).
- Parameters:
    FirstName: First name of the user.
    LastName: Last name of the user.
    Email: User email.
    Age: User age.
    Salary: User salary.
    Department: Department of the user.

```json
{
  "command": "fillUserForm",
  "parameters": {}
}
```

5. **submitForm**
- Description: This command submits the form with the filled user data.
- Usage:
    submitForm: Submits the filled form.
- Parameters: None.

```json
{
  "command": "submitForm",
  "parameters": {}
}
```

6. **waitForTableRowData**
- Description: This command waits for a newly added user record to appear in the table.
- Usage:
    waitForTableRowData: Waits for the table to refresh with the new user data.
- Parameters: None.

```json
{
  "command": "waitForTableRowData",
  "parameters": {}
}
```

7. **clickEdit**
- Description: This command clicks the "Edit" button for an existing user record.
- Usage:
    clickEdit: Clicks the "Edit" button for the specified user.
- Parameters:
    email: The email of the user to be edited.

```json
{
  "command": "clickEdit",
  "parameters": {
    "email": "test@oleh.com"
  }
}
```

8. **editUserSalary**
- Description: This command edits the salary for an existing user.
- Usage:
    editUserSalary: Updates the salary of the selected user.
- Parameters:
    newSalary: The new salary value to update.

```json
{
  "command": "editUserSalary",
  "parameters": {
    "newSalary": "90000"
  }
}
```

9. **clickDelete**
- Description: This command clicks the "Delete" button to remove an existing user.
- Usage:
    clickDelete: Deletes the user specified by the email address.
- Parameters:
    email: The email of the user to be deleted.

```json
{
  "command": "clickDelete",
  "parameters": {
    "email": "test@oleh.com"
  }
}
```

10. **waitForTableRowDeletion**
- Description: This command waits for a user to be removed from the table after clicking the delete button.
- Usage:
    waitForTableRowDeletion: Waits for the table to update after a user is deleted.
- Parameters:
    email: The email of the user to be deleted.

```json
{
  "command": "waitForTableRowDeletion",
  "parameters": {
    "email": "test@oleh.com"
  }
}
```

## Host and Service Configuration with Dependency Injection and Logging

### HostBuilderSetup
The project uses **Dependency Injection (DI)** and **Logging** in the ```csharp HostSetup class```. This setup is done using the ```csharp Host.CreateDefaultBuilder()``` method, where we configure services, logging, and other dependencies.

Here’s a brief summary of how services are configured:

**Configuration Services:**
- appsettings.json is loaded to retrieve configuration settings.
- Services for each command (OpenUrlCommand, RemoveAdsCommand, etc.) are registered.

**Logging:**
- Console logging is configured with Information level logs.
- Logs for each command are captured through the ILogger interface injected into the commands.

**Service Registration:**
- Command Factory is registered to dynamically create command instances.
- Commands such as OpenUrlCommand, ClickAddNewRecordButtonCommand, etc., are registered as singleton services.

## Running the Project
Clone the repository to your local machine.
Run ```bash dotnet restore``` to restore the NuGet packages.
Run ```bashdotnet build``` to build the project.
Execute the project using ```bash dotnet run``` to run the automation script.