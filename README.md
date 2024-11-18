---

# Municipal Services Application

### Overview
The Municipal Services Application is a WPF-based software solution designed to streamline the management of municipal services. Built using the .NET Framework 4.8 and following the MVVM (Model-View-ViewModel) design pattern, the application offers citizens a seamless experience for reporting issues, viewing local events, tracking service requests, and receiving recommendations. The goal is to enhance user engagement by providing an organized platform for efficient communication between citizens and municipal authorities.

---

## Features

### Core Features:

- **Service Request Management:**  
  - The core functionality of the application is managing and tracking service requests submitted by users.  
  - Users can view all requests in an organized DataGrid, where each request includes essential details such as request ID, title, status, priority, date submitted, and progress.  
  - The application utilizes advanced data structures like Binary Search Trees for efficient search operations, Max Heaps for prioritizing tasks, and Graphs for managing dependencies between service requests.  
  - Users can create new requests with detailed fields like title, status, priority, and dependencies, ensuring each request is accurately categorized and prioritized.  
  - Deletion functionality allows users to remove completed or invalid requests, keeping the system up-to-date.  
  - All new or updated requests are seamlessly integrated into the underlying data structures, ensuring real-time updates in both UI and internal processing.  
  - The "Prioritized Requests" tab dynamically displays high-priority tasks for easy accessibility, while the "Request Dependencies" tab visually maps the relationships between tasks.  

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
  - Provides the foundational platform for building and running Windows desktop applications, ensuring compatibility and robustness.  

- **WPF (Windows Presentation Foundation)**  
  - Enables the creation of visually appealing, modern, and responsive desktop applications.  
  - Offers seamless integration of UI and business logic, leveraging XAML for rich UI designs.  

- **MVVM (Model-View-ViewModel) Design Pattern**  
  - Ensures a clean separation of concerns, enhancing testability, maintainability, and scalability.  
  - Facilitates the binding of UI components to data and commands for a streamlined user experience.  

- **Advanced Data Structures:**  
  - **Binary Search Trees (BST):** Optimized for efficient searching and retrieval of service requests by unique identifiers.  
  - **Max Heaps:** Used to prioritize service requests based on urgency, ensuring critical tasks are addressed first.  
  - **Graphs:** Manages and visualizes dependencies between service requests, supporting complex relationships.  
  - **Priority Queues:** Enhances task scheduling by ranking service requests based on predefined priorities.  
  - **Hash Tables:** Provides fast lookups for user data and service requests.  
  - **Dictionaries & Sorted Dictionaries:** Organizes and sorts local events and announcements for efficient display and filtering.  
  - **Stacks & Queues:** Supports specific operations like undo/redo features and task processing in a first-in, first-out (FIFO) manner.  
  - **Sets:** Ensures unique entries for categories, events, and service request identifiers to avoid duplication.  

This combination of technologies and data structures ensures the application is highly efficient, scalable, and user-friendly.

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

## Data Structures in Detail

The application leverages several advanced data structures to efficiently manage service requests, prioritize tasks, and handle dependencies. Each data structure plays a critical role in optimizing the performance and usability of the application.

### 1. **Binary Search Tree (BST)**

#### **Role**:
- Stores service requests, enabling efficient searching, insertion, and deletion operations.
- Specifically used to quickly search for a request by its unique ID, improving the performance of the "Search BST" functionality.

#### **Advantages**:
- **Search Efficiency**: Time complexity is \(O(\log n)\) for search operations in a balanced BST.
- **Scalability**: Handles large datasets efficiently compared to linear search methods.

#### **Example in Application**:
When a user searches for a request by ID, the application leverages the BST to quickly locate the request without iterating through the entire list.

---

### 2. **Max Heap**

#### **Role**:
- Maintains prioritized service requests, ensuring the request with the highest priority is always at the root.
- Used in the "Prioritized Requests" tab to display requests based on their importance or urgency.

#### **Advantages**:
- **Optimal Retrieval**: Always retrieves the highest-priority request in \(O(1)\) time.
- **Dynamic Management**: Allows for insertion and removal of requests while maintaining the heap property.

#### **Example in Application**:
If a new high-priority request (e.g., "Flood Control") is added, the heap ensures it is appropriately placed at the top for immediate attention.

---

### 3. **Graph**

#### **Role**:
- Represents dependencies between service requests as a directed graph.
- Each node corresponds to a request, and edges represent dependencies between them.

#### **Advantages**:
- **Dependency Management**: Allows visualization of request dependencies, ensuring dependent tasks are completed in the correct order.
- **Topological Sorting**: Provides a sequence in which requests should be processed based on their dependencies.

#### **Example in Application**:
If "Water Supply Issue" depends on resolving "Sewer Backup," the graph ensures this relationship is represented, and the TreeView visualization displays it effectively.

---

### 4. **Priority Queue**

#### **Role**:
- Internally implemented using a heap to handle recommended events and announcements.
- Ensures that events or announcements with the highest user interaction are shown at the top.

#### **Advantages**:
- **Efficient Organization**: Keeps frequently interacted events prioritized for display.
- **Dynamic Updates**: Allows real-time adjustments based on user engagement.

#### **Example in Application**:
If users frequently search for "Music Events," these events are prioritized in the recommendation tab.

---

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

--- 
