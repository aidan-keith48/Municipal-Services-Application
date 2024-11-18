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

###4. **Service Request Management:
    Navigate to the "Service Request Status" tab to view, create, or manage service requests.
    View Requests:
    All service requests are displayed in a DataGrid with columns like Request ID, Title, Status, Priority, and Progress.
    Search Requests:
    Use the "Search BST" functionality to search for a specific request by its ID. The Binary Search Tree ensures efficient and fast lookups.
    Create New Requests:
    Fill in the required fields: Title, Status, Priority, and Dependencies.
    Newly created requests are added to the Max Heap for prioritization and the BST for fast retrieval.
    Manage Dependencies:
    The "Request Dependencies" tab allows you to visualize dependencies using a Graph. Use the TreeView to explore the hierarchical relationships between tasks.

---
## Data Structures in Detail

The application leverages advanced data structures to efficiently manage service requests, prioritize tasks, and handle dependencies. Below is an in-depth look at each data structure, its role in the application, and how it is implemented and integrated.

---

### 1. **Binary Search Tree (BST)**

#### **Role**:
- The Binary Search Tree (BST) is used to store and retrieve service requests based on their unique `RequestId`.
- It optimizes search operations, making the "Search BST" functionality efficient.

#### **Implementation**:
- The `BinarySearchTree` class is initialized during the repository setup in the `InitializeBST` method.
- All existing service requests are inserted into the BST when initialized.
- When a new service request is added, it is also inserted into the BST (`_bst.Insert(request)`).

#### **Integration**:
- **Repository Integration**:
  - The BST is updated during the `AddServiceRequest` method, ensuring the new request is searchable immediately.
  - The `SearchInBST(int requestId)` method in the repository performs a direct search in the BST.
- **ViewModel Integration**:
  - The `SearchUsingBST` command in the `ServiceRequestStatusViewModel` uses the repository’s `SearchInBST` method.
  - Example:
    ```csharp
    var result = _repository.SearchInBST(requestId);
    if (result != null)
    {
        ServiceRequests.Clear();
        ServiceRequests.Add(result);
    }
    ```

#### **Advantages**:
- **Search Efficiency**: The time complexity for search operations is \(O(\log n)\) in a balanced BST.
- **Dynamic Updates**: Any new, updated, or deleted requests are immediately reflected in the BST.

#### **Example**:
When a user searches for a request with ID `17`, the application queries the BST, quickly locating the request without iterating over the entire list.

---

### 2. **Max Heap**

#### **Role**:
- The Max Heap ensures that the service request with the highest priority is always at the root.
- It is used in the "Prioritized Requests" tab to display requests in descending order of priority.

#### **Implementation**:
- The `MaxHeap` class is initialized during the repository setup in the `InitializeHeap` method.
- All existing service requests are added to the heap during initialization.
- New requests are inserted into the heap using `_maxHeap.Insert(request)`.

#### **Integration**:
- **Repository Integration**:
  - The heap is updated dynamically during the `AddServiceRequest` method.
  - Prioritized requests are extracted from the heap using the `GetPrioritizedRequests` method, which clears and refills the heap to maintain its structure.
- **ViewModel Integration**:
  - The `LoadPrioritizedRequests` command in the `ServiceRequestStatusViewModel` fetches prioritized requests from the repository.
  - Example:
    ```csharp
    foreach (var request in _repository.GetPrioritizedRequests())
    {
        PrioritizedRequests.Add(request);
    }
    ```

#### **Advantages**:
- **Dynamic Prioritization**: Automatically adjusts the highest-priority request as new tasks are added or removed.
- **Efficient Retrieval**: The highest-priority request can be accessed in \(O(1)\) time.

#### **Example**:
If a new request with priority `100` is added, it will automatically appear at the top of the "Prioritized Requests" tab.

---

### 3. **Graph**

#### **Role**:
- The Graph represents dependencies between service requests.
- Each node corresponds to a service request, and edges represent dependencies between them.

#### **Implementation**:
- The `Graph` class is initialized during the repository setup in the `InitializeGraph` method.
- Dependencies are added using the `_graph.AddEdge(request.RequestId, dependency)` method.
- The graph supports operations like dependency retrieval (`GetDependencies`) and topological sorting (`TopologicalSort`).

#### **Integration**:
- **Repository Integration**:
  - Dependencies are added to the graph during the `AddServiceRequest` method.
  - The `GetDependencies(int requestId)` method retrieves dependencies for a specific request.
- **ViewModel Integration**:
  - The `LoadDependencies` command in the `ServiceRequestStatusViewModel` fetches dependencies for a selected request.
  - Example:
    ```csharp
    foreach (var dep in dependencies)
    {
        var request = _repository.GetRequestById(dep);
        if (request != null)
        {
            DependencyRequests.Add(request);
        }
    }
    ```

#### **Advantages**:
- **Dependency Management**: Helps in visualizing and managing tasks that depend on each other.
- **Topological Sorting**: Ensures requests are processed in the correct order, respecting dependencies.

#### **Example**:
If "Water Supply Issue" depends on "Sewer Backup," this relationship is represented as an edge in the graph and visualized in the "Request Dependencies" tab.

---

### 4. **Priority Queue**

#### **Role**:
- A priority queue (backed by the Max Heap) handles tasks and events requiring priority-based management.

#### **Implementation**:
- The priority queue leverages the Max Heap, ensuring efficient insertion and retrieval of high-priority items.

#### **Integration**:
- The priority queue is indirectly used in the "Prioritized Requests" tab through the Max Heap.
- Recommendations and event prioritization also leverage this data structure.

#### **Advantages**:
- **Efficiency**: Ensures the highest-priority tasks/events are displayed first.
- **Scalability**: Can handle dynamic additions and updates without degrading performance.

#### **Example**:
In the "Prioritized Requests" tab, the queue ensures that tasks like "Flood Control" appear higher due to their critical nature.

---

### 5. **Dictionaries**

#### **Role**:
- Dictionaries are used to store and quickly retrieve service requests, events, and announcements based on unique keys.

#### **Integration**:
- **Repository Integration**:
  - The `GetRequestById(int requestId)` method retrieves a request using LINQ but could be optimized using a dictionary.
- **Advantages**:
  - **Fast Lookups**: Average time complexity of \(O(1)\) for retrieval operations.
  - **Ease of Use**: Simplifies data storage and retrieval.

---

## Integration Summary

The repository acts as a bridge, integrating these data structures to manage service requests effectively:

1. **Adding a Request**:
   - The request is added to the in-memory list (`_serviceRequests`).
   - It is also inserted into the BST and Max Heap for efficient searching and prioritization.
   - Dependencies are added to the graph.

2. **Searching for a Request**:
   - The ViewModel invokes the repository's `SearchInBST` or `SearchRequests` methods.
   - The repository queries the BST or performs a linear search based on the use case.

3. **Prioritizing Requests**:
   - Requests are managed dynamically in the Max Heap.
   - The "Prioritized Requests" tab retrieves and displays these in order of importance.

4. **Visualizing Dependencies**:
   - The graph ensures that dependencies are clearly mapped.
   - The TreeView in the "Request Dependencies" tab displays these relationships hierarchically.

This seamless integration between the repository, ViewModel, and advanced data structures ensures a robust and user-friendly application.
---

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

--- 
