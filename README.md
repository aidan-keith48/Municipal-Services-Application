# Municipal Services Application

### Overview
The Municipal Services Application is a WPF-based software solution designed to streamline the management of municipal services. Built using the .NET Framework 4.8 and following the MVVM (Model-View-ViewModel) design pattern, the application offers citizens a seamless experience for reporting issues, viewing local events, tracking service requests, and receiving recommendations. The goal is to enhance user engagement by providing an organized platform for efficient communication between citizens and municipal authorities.

---

## Features

### Core Features:
- **Report Issues:** 
  - Users can report various municipal issues such as sanitation, road damage, utility failures, and more.
  - The system allows users to specify details like location, category, and description, ensuring accurate and clear reports.
  - Input validation ensures that only complete and valid information is submitted.
  - Users can also attach various file types, including images, documents, and PDFs, for better clarification.

- **Attach Media:**
  - Users can attach images and other file types (such as PDFs, Word documents, Excel sheets, and PowerPoint presentations) to their reports.
  - Supported formats include `.jpg`, `.jpeg`, `.png`, `.bmp`, `.pdf`, `.doc`, `.docx`, `.xls`, `.xlsx`, `.ppt`, and `.pptx`.

- **Local Events & Announcements:**
  - The application displays upcoming local events and announcements in an aesthetically organized way.
  - Users can search for events by categories (e.g., Music, Sports, Technology) or filter by dates.
  - Advanced data structures such as sorted dictionaries and priority queues are used to manage and display event-related data efficiently.
  - Announcements keep users informed about important updates, such as maintenance schedules, local government alerts, and more.

- **Search & Recommendations:**
  - Users can search for local events based on specific categories and dates.
  - The application analyzes search patterns and suggests recommended events, helping users discover relevant events they might be interested in.
  - Recommendations are displayed based on user interactions and preferences, though they do not persist beyond the current session.

- **User Engagement:**
  - The application includes a responsive, vertical navigation panel for easy access to all features.
  - Push notification strategies and recommendation features keep users engaged and informed.

---

## Tech Stack
- **.NET Framework 4.8**
- **WPF (Windows Presentation Foundation)**
- **MVVM (Model-View-ViewModel) Design Pattern**
- **Advanced Data Structures:** Utilizes stacks, queues, priority queues, hash tables, dictionaries, sorted dictionaries, and sets for efficient data management.

---

## Installation & Setup

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/your-username/municipal-services-app.git
   ```

2. **Open the Solution:**
   - Open the solution file (`Municipal_Services_Application.sln`) in Visual Studio 2019 or later.

3. **Build the Project:**
   - Ensure you have .NET Framework 4.8 installed on your system.
   - Build the project using the Build menu in Visual Studio.

4. **Run the Application:**
   - Press `F5` or click the "Start" button in Visual Studio to run the application.

---

## Usage

### 1. **Report an Issue:**
   - Navigate to the "Report Issues" section.
   - Fill in the required fields:
     - **Location:** Enter a location between 5 and 100 characters.
     - **Category:** Choose from a variety of predefined categories like Sanitation, Roads, Utilities, etc.
     - **Description:** Provide a detailed description of the issue, with at least 10 characters.
   - Optionally, attach any relevant files (images, documents, etc.).
   - Click **Submit Report** to save your entry. A confirmation message will be displayed.

### 2. **View Submitted Reports:**
   - Navigate to the "View Reports" section to see all previously submitted issues, including the location, category, description, and attached files.

### 3. **Browse Events & Announcements:**
   - Select the "Local Events & Announcements" option from the main menu.
   - Browse through upcoming local events and important announcements.
   - Use the search functionality to filter events based on categories and dates.
   - View recommended events that match your search patterns.

---

## Folder Structure

```
|-- Municipal_Services_Application
    |-- Model                # Contains model classes (e.g., IssueTicket, LocalEventsModel)
    |-- ViewModels           # Contains ViewModel classes (e.g., ReportIssueViewModel, LocalEventsViewModel)
    |-- Views                # Contains XAML Views (e.g., ReportIssuesView, LocalEventsView)
    |-- Styling              # Contains resource dictionaries for styling the app
    |-- App.xaml             # Entry point for WPF resources
    |-- MainWindow.xaml      # Main application window
    |-- README.md            # This file
```

---

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
