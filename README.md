# SignalR Discount Code Handler

This project is a SignalR-based application for generating and using unique discount codes. It uses LiteDB for data storage and ASP.NET Core SignalR for real-time communication.

## Features

- Generate unique discount codes
- Use and invalidate discount codes
- Real-time notifications to all connected clients

## Prerequisites

- .NET 8 SDK

## Getting Started

1. **Clone the repository:**
```
git clone https://github.com/your-repo/signalr-discount-code-handler.git
cd signalr-discount-code-handler
```

2. **Run the application:**

    Run the application. The application will start and be accessible at `http://localhost:5092`.

3. **Open the html file:**

    Open the html file "client.html" located in the root folder to start the client side.

4. **Enjoy the app:**

    Observe responses from the app in the console.

5. **Browse DB to observe generation and usage of discount codes:**

    Use LiteDB Studio to open codes.db file located in the root folder. You can observe the generation and usage of discount codes in the database. Don't forget to Disconnect from the DB in LiteDB Studio before running some application action again.

    
    