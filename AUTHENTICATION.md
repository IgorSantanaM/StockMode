# Duende Identity Provider Integration

This document explains the authentication implementation using Duende IdentityServer that has been integrated into the StockMode application.

## Overview

The authentication system consists of three main components:

1. **Identity Provider (IDP)** - Duende IdentityServer running on port 5001
2. **Frontend** - React application with OIDC authentication
3. **WebAPI** - Backend API with JWT Bearer authentication

## Architecture

```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   Frontend      │    │   Identity      │    │   WebAPI        │
│   (React)       │    │   Provider      │    │   (ASP.NET)     │
│   Port 80/5173  │    │   Port 5001     │    │   Port 8080     │
└─────────────────┘    └─────────────────┘    └─────────────────┘
         │                       │                       │
         │ 1. Auth redirect      │                       │
         ├──────────────────────►│                       │
         │                       │                       │
         │ 2. Auth code          │                       │
         │◄──────────────────────┤                       │
         │                       │                       │
         │ 3. Token exchange     │                       │
         ├──────────────────────►│                       │
         │                       │                       │
         │ 4. Access token       │                       │
         │◄──────────────────────┤                       │
         │                                               │
         │ 5. API calls with Bearer token               │
         ├──────────────────────────────────────────────►│
         │                                               │
         │ 6. Protected resource response               │
         │◄──────────────────────────────────────────────┤
```

## Configuration

### Identity Provider (IDP)

Located in `/StockMode.IDP/`, the IDP is configured with:

- **Client ID**: `stockmodeclient`
- **Scopes**: `openid`, `profile`, `email`, `stockmodeapi`
- **Grant Type**: Authorization Code Flow
- **Test Users**: 
  - Username: `GoodCompany`, Password: `password`
  - Username: `CoolCompany`, Password: `password`

### Frontend Configuration

The React app (`/frontend/src/stockmode.frontend/`) uses:

- **OIDC Library**: `react-oidc-context` and `oidc-client-ts`
- **Authority**: `http://localhost:5001` (dev) or `http://stockmode.idp` (container)
- **Client ID**: `stockmodeclient`
- **Redirect URI**: `/signin-oidc`

### API Configuration

The WebAPI (`/src/StockMode.WebApi/`) validates JWT tokens from the IDP:

- **Authority**: `http://stockmode.idp`
- **Audience**: `stockmodeapi`
- **Authentication Scheme**: JWT Bearer

## How It Works

1. **User Access**: When a user tries to access the application, they're presented with a login button
2. **Authentication**: Clicking login redirects to the IDP at `/Account/Login`
3. **Credentials**: User enters credentials (GoodCompany/password or CoolCompany/password)
4. **Authorization**: IDP redirects back to frontend with authorization code
5. **Token Exchange**: Frontend exchanges code for access token
6. **API Calls**: All API requests include the Bearer token in the Authorization header
7. **Token Validation**: WebAPI validates the token against the IDP

## Test Users

Two test users are configured in the IDP:

| Username | Password | Email |
|----------|----------|-------|
| GoodCompany | password | igorsantanamedeiros17@gmail.com |
| CoolCompany | password | igorsantanamedeiros@outlook.com |

## Testing the Integration

### Local Development

1. Start the IDP:
   ```bash
   cd StockMode.IDP
   dotnet run
   ```

2. Start the WebAPI:
   ```bash
   cd src/StockMode.WebApi
   dotnet run
   ```

3. Start the Frontend:
   ```bash
   cd frontend/src/stockmode.frontend
   npm run dev
   ```

4. Navigate to `https://localhost:5173`
5. Click "Fazer Login"
6. Enter credentials: `GoodCompany` / `password`
7. Access `/auth-test` page to see authentication details

### Docker Environment

1. Build and run with docker-compose:
   ```bash
   docker-compose up --build
   ```

2. Navigate to `http://localhost`
3. Follow the same login process

### API Endpoints

- **Public**: `GET /api/user/test` - No authentication required
- **Protected**: `GET /api/user/info` - Requires authentication
- **Protected**: All `/api/products/*` endpoints - Require authentication

## Key Features

- ✅ **Authentication Guard**: App requires login before access
- ✅ **User Information**: Header displays authenticated user's name
- ✅ **Automatic Token Injection**: API calls automatically include Bearer token
- ✅ **Token Refresh**: Automatic silent token renewal
- ✅ **Logout**: Clean logout with redirect
- ✅ **CORS Configuration**: Proper cross-origin support
- ✅ **Container Support**: Works in both development and containerized environments

## Client-Specific Information Flow

1. **Identity Provider** provides user claims (name, email, company)
2. **Frontend** receives and stores user profile information
3. **Header Component** displays user-specific information (name from claims)
4. **API Calls** include user context through JWT token
5. **Backend** can access user claims for business logic

The user's company information (GoodCompany/CoolCompany) flows through the entire system via the JWT token claims, allowing for client-specific functionality throughout the application.