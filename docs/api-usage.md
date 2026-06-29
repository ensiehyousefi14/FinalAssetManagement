# ⚙️ API Usage & Testing

This document demonstrates how to interact with the API endpoints, handle authentication, and read standard responses.

## 🔑 Authentication Flow
The API uses **JWT (JSON Web Token)** for secure authorization. Users must authenticate first to obtain a bearer token, which must be included in the headers of all protected requests.

| Login API Endpoint | Handling Unauthorized Access |
| :---: | :---: |
| ![Login API](../images/login-api.png) | ![Unauthorized](../images/unauthorized.PNG) |

---

## 📊 Asset Management Endpoints
All API endpoints return a standardized JSON response structure containing metadata, success status, and the payload.

### Example: Get Assets Endpoint
Below is an example of fetching assets with a standardized API response:

![Get Assets](../images/get-assets.png)
