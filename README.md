# Vehicle Management System (VMS)

## Project Overview
The Vehicle Management System (VMS) is a web-based platform designed to manage and track the vehicles (cars, buses, bikes) of a specific company. It supports functionalities for both administrators and users, ensuring seamless management and an intuitive user experience. 

### Key Features
- **Vehicle Management**: Add, edit, delete, and view vehicles and their details.
- **Component Management**: Track components such as brand, model, engine, doors, glass, color, etc.
- **Engine Replacement Options**: View and manage a list of possible engine replacements for specific vehicles, based on power compatibility.
- **Filtering and Searching**:
  - Admin can filter vehicles by categories (cars, buses, bikes).
  - Users can search vehicles based on specific preferences and view matching results.
- **User Preferences**: Registered users can save their preferences for easier searches.
- **Speed Display**: Show the speed of vehicles based on location:
  - UK: MPH
  - USA: KMPH
- **Availability Tracking**: Admin can monitor the availability status of vehicles (available or sold out).

## System Functionalities

### Admin Functionalities
- **Authentication**:
  - Sign in with username and password.
- **Vehicle Management**:
  - Add new vehicles with detailed information (e.g., brand, model, engine, doors, price).
  - Edit existing vehicle details.
  - Delete vehicles.
  - View details of vehicles.
  - Monitor vehicle status (available/sold out).
- **Engine Management**:
  - View possible engine replacements for specific vehicles.
- **Filtering**:
  - Filter vehicles by categories (cars, buses, bikes).

### User Functionalities
- **Search**:
  - Search for vehicles based on specific preferences (e.g., brand, model, engine power, color).
  - View a dashboard with a vehicle list matching their preferences.
- **Preferences**:
  - Save personal preferences if logged in.
- **Speed Display**:
  - Speed shown in MPH for UK users and KMPH for USA users.

## Technologies and Tools Used

### Back-end
- **Languages and Frameworks**:
  - ASP.NET
  - C#
  - JavaScript
  - MVC (Model-View-Controller) Architecture
  - CSHTML (Razor Markup Engine for server-side rendering)
- **Database**:
  - Microsoft SQL Server

### Front-end
- **Frameworks and Languages**:
  - Bootstrap
  - HTML
  - CSS (Cascading Style Sheets)

### Integrated Development Environment (IDE)
- Visual Studio 2022
- SQL Server Management Studio (SSMS)

## Installation and Setup

### Prerequisites
1. Visual Studio 2022
2. SQL Server Management Studio (SSMS)
3. .NET Framework installed on the server

### Steps
1. Clone the repository from the source.
2. Open the project in Visual Studio 2022.
3. Restore NuGet packages if required.
4. Configure the database connection string in the `appsettings.json` file.
5. Use SQL Server Management Studio to create the required database and tables using the provided SQL scripts.
6. Run the application in Visual Studio and navigate to the provided localhost URL.

## Usage

### Admin Access
1. Sign in with the admin credentials.
2. Manage vehicles, components, and engine options.
3. Filter and monitor vehicle categories and statuses.

### User Access
1. Browse or search for vehicles based on preferences.
2. Save preferences if logged in.
3. View speed information based on location.


---

**Developed By**: Tony Riad 
