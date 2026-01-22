# Online Store - Cloud Database Project

**Student Name:** Lokman   
**Course:** Cloud Computing  
**Date:** January 2026

## About This Project

This is my assignment project for cloud computing minor course. I created an online store system that can handle many customers at same time using cloud technology.

## My Design Choices

### Why I Chose Different Databases

**1. SQL Database - For Product Information**
- I keep product details in SQL database
- Products have fixed structure (name, price, stock)
- Easy to update and search products
- Good for structured data

**2. Table Storage - For Customer Orders**
- I use Table Storage because our store gets many orders
- During busy time, many customers place orders together
- Table Storage can handle this high traffic
- I partition orders by customer email so search is fast
- Cost is lower than SQL for millions of orders

**3. Cosmos DB - For Customer Reviews**
- Reviews are different from each other
- Some reviews are short, some are long
- Cosmos DB is flexible for this type of data
- Good for anonymous reviews from customers

### CQRS Pattern (Read and Write Separation)

I separate reading data and writing data:
- **Writing (Commands):** When customer creates new order
- **Reading (Queries):** When customer checks their orders
- This separation helps the system work faster
- During busy hours, many people can read orders at same time

### N-Tier Architecture (Three Layers)

My project has three layers:
1. **Data Layer:** Connects to all databases
2. **Business Layer:** Has the business rules and validation
3. **Function Layer:** Does background work like processing orders

This design makes testing easier and code is more organized.

### Loose Coupling

I use interfaces (IOrderService, IReviewService) so:
- Each part can work independently
- Easy to test each service separately
- Can change one part without breaking others
- This is good practice for professional projects

## Azure Functions (Background Work)

I created 2 Azure Functions:

**Function 1: Order Processing**
- Runs every 5 minutes automatically
- Checks for new orders
- Processes pending orders
- Changes order status from "Pending" to "Processing"

**Function 2: User Data Sync**
- Runs every 5 minutes automatically  
- Collects user information from orders
- Sends data to partner company (Wiley & Co)
- Keeps both systems synchronized

## How to Run My Project

### What You Need:
- Visual Studio 2022
- Azurite (Azure Storage Emulator)
- Cosmos DB Emulator

### Steps to Run:
1. Open command prompt and type: `azurite`
2. Start Cosmos DB Emulator from Windows start menu
3. Open project in Visual Studio
4. Press F5 to run Web API
5. Right-click WidgetStore.Functions → Debug → Start New Instance

### How to Test:
1. Go to: http://localhost:5000/swagger
2. Try creating an order
3. Try getting order by user email
4. Try posting a review
5. Check Functions console to see background processing


## Problems I Faced

**Problem 1:** First time Azure Functions gave "System.Memory.Data" error
- **Solution:** I changed the code to simpler version that works

**Problem 2:** Cosmos DB connection was failing
- **Solution:** Made sure Cosmos DB Emulator was running before starting project

**Problem 3:** Entity Framework version mismatch
- **Solution:** Changed package version to match .NET 8.0


## My Understanding

I understand that:
- Table Storage is better for high volume data like orders
- Cosmos DB is better for flexible data like reviews
- SQL is better for structured data like products
- CQRS helps system perform better under load
- Azure Functions are good for background tasks
- Loose coupling makes code easier to maintain

This project taught me practical cloud database concepts that are used in real companies.

---

**Note:** This is my assignment work. I designed and implemented everything based on my understanding of cloud database concepts.

**Contact:** [Your Email if needed]
