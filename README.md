# MusicStore
MusicStore Web Application
Final Project — Application Design II (CWEB2226)

Project Overview
MusicStore is a full‑stack web application built using the ASP.NET MVC design pattern. It demonstrates the integration of front‑end and back‑end systems, secure coding practices, object‑oriented programming, and database connectivity. The application allows users to browse musical instruments while providing administrators with full CRUD capabilities through a secure admin panel.

This project fulfills all requirements for the Application Design II final project, including file uploads, validation, authentication, search, sort, pagination, and role‑based access control.

Features
Input and Upload
Upload instrument images (JPG/PNG)

Text input fields for Name, Brand, Category, Price, and Description

Validations
Server‑side validation using DataAnnotations

Client‑side validation using jQuery Unobtrusive Validation

Security
Secure login system using ASP.NET Identity

Password hashing and salted storage

Role‑based access control (Admin, User)

Admin panel protected with authorization attributes

Secure database connection string

UI and UX
Responsive layout using Bootstrap 5

Product grid with images

Category filtering

Search functionality

Sorting (price ascending/descending, name ascending/descending)

Pagination with preserved filters

CRUD Operations
Administrators can:

Create new instruments

Edit existing instruments

Upload or replace images

Delete instruments

View all products in a table format

Architecture
Design Pattern
The application follows the MVC (Model‑View‑Controller) pattern.

Models
Instrument

PagingInfo

InstrumentsListViewModel

ASP.NET Identity models (User, Roles)

Views
Razor views for browsing, CRUD operations, login, and shared layout

Controllers
InstrumentController (public browsing)

AdminController (CRUD operations)

AccountController (authentication)

Repository Pattern
The application uses an abstraction layer for data access:

IInstrumentRepository

EFInstrumentRepository (Entity Framework implementation)

This improves maintainability and testability.

Security Implementation
ASP.NET Identity for authentication and authorization

Password hashing and secure storage

Role‑based access control

Anti‑forgery tokens on all POST requests

Restricted admin panel

Secure database connection string

Database
SQL Server (LocalDB or full SQL Server)

Tables include:

Instruments

AspNetUsers

AspNetRoles

AspNetUserRoles

Image data stored as byte arrays

A database export (.sql) is included in the project ZIP

How to Run the Project
Clone the repository:

Code
git clone <your-repo-url>
Open the solution in Visual Studio.

Restore NuGet packages.

Update the connection string in Web.config if needed.

Run the application using IIS Express.

Log in using the seeded admin account:

Code
user: admin
Password: secret

Technologies Used
C# / ASP.NET MVC 5

Entity Framework 6

SQL Server

Bootstrap 5

jQuery

Razor Views

ASP.NET Identity

Author
Korbin
Application Design II — Final Project
Spring 2026