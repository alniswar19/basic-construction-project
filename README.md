# Getting Started

# Prerequisites
Ensure you have the following software installed on your system:
- Docker
- VS2022 + .NET 8

# Installation
*Clone the repository:*

`git clone https://github.com/yourusername/net-aspire.git`

*Build and Run*

# FAQ

- Q: Why the response show about db connection?
- A: The database container is being spun while the API already up and running. Currently it's still an open PR (https://github.com/dotnet/aspire/issues/5275) we need the statement to waitFor another resource.

- Q: Why my data is gone when i rerun the project?
- A: The current implementatation has no Bind Data volume, you can enable it in the apphost if needed.
