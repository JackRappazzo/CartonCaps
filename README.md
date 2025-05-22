# Referral API
## User Controller
---

### Get Referral Code
> Returns the referral code and referral link to be used in the 'share' feature
These are both unique to the user

#### Request `GET /api/users/referralCode`
##### Headers

```
Content-Type: application/json
Authorization: Bearer <access-token-here>
```

#### Response `200 OK`
```
{
    "referralCode": "ABC123D",
    "deferredLink": "https://sample.com/abc123?referral_code=ABC123D"
}
```

### Get Referrals
> Returns a page from a collection of ReferredUsers. ReferredUser includes the user's name and their ReferrerStatus
Here, pageStart refers to the index in the sequence to start collecting from. For example, pageStart=2 would start at index 2 (record #3).
This can be used for traditional paging or for lazy loading


#### Request `GET /api/users/referredUsers?numberPerPage={size}&pageStart={start}`
#### Headers
```
Content-Type: application/json
Authorization: Bearer <access-token-here>
```

#### Response `200 OK`
```
{
    "refferedUsers": [
        {
            "referringUserId": "2a154b8b-9228-4701-9e57-b538a66796e0",
            "truncatedName": "Aoife N.",
            "referralState": "Completed",
            "createdOn": "2025-02-10T12:02:58.4095781-05:00"
        },
        {
            "referringUserId": "2a154b8b-9228-4701-9e57-b538a66796e0",
            "truncatedName": "Hien N.",
            "referralState": "Completed",
            "createdOn": "2025-04-04T12:02:58.4095754-04:00"
        },
        ...
        {
            "referringUserId": "2a154b8b-9228-4701-9e57-b538a66796e0",
            "truncatedName": "Sam P.",
            "referralState": "Pending",
            "createdOn": "2025-05-01T12:02:58.4094476-04:00"
        }
    ],
    "total": 16,
    "pageStart": 0,
    "numberPerPage": 5
}
```

#### Errors
`400 Bad Request`
```
{
    "errors": {
        "pageStart": [
            "The value 'a' is not valid."
        ]
    },
    "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "traceId": "00-349bc8188db2635800fa4072f5f2c58b-9d4081fe159b6843-00"
}
```

## Deferred Link Controller
---

### Resolve Deferred Link
> Accepts the deferred link's unique code and returns its intended destination and its metadata
The unique code is the Base62 string of characters at the end of the URI but before the query parameters
Example: `https://cartoncaps.app/link/abc123EF?referral_code=ghIJk1234`
abc123EF is the link's code

#### Request `GET /api/deferredLinks/resolve/{linkCode}`
#### Headers
```
Content-Type: application/json
```
#### Response `200 OK`
```
{
    "destination": "registration/referral",
    "metadata": {
        "ReferralCode": "ABC123de"
    }
}
```
#### Errors
`404 Not Found`

## General Errors
---
### JWT Expired - Not Implemented
> When the JWT expires we expect to see a 401 with the following message. If the RefreshToken is still valid, calling `/users/refreshJwt` will return a fresh JWT.

```
{
    "authentication_error" : "jwt_expired",
}
```

### Internal Server Error
> Any uncaught error will be automatically logged and the service will return the below message. In a future state we could return more detailed messages in the development environment

`500`
```
{
	"message": "An error occurred while processing the request"
}
```
