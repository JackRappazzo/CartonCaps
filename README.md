# Referral API
## User Controller
---
### Complete Profile
#### Request `POST /api/users/completeProfile`
```
Headers
Content-Type: application/json
Authentication: Bearer <access-token-here>
```
##### Body

```
{
    "firstName": "Sam",
    "lastName": "Doe",
    "referralCode" : "ABC123D",
    "birthDate" : "2000-01-30",
    "zipCode" : 123456
}
```

#### Response
`200 OK`

#### Error
`400 Bad Request`
```
{
    "errors" : [
        {
            "fieldName" : "birthDate",
            "errorMessage": "Invalid format"
        },
        {
            "fieldName" : "zipcode",
            "errorMessage": "Missing"
        },
        {
            "fieldName": "referralCode",
            "errorMessage": "Code is expired or does not exist"
        }
    ]
}
```

### Get Referral Code
> Returns the referral code and referral link to be used in the 'share' feature

#### Request `GET /api/users/referralCode`
##### Headers

```
Content-Type: application/json
Authentication: Bearer <access-token-here>
```

#### Response `200 OK`
```
{
    "referralCode": "ABC123D",
    "referralDeepLink": "https://sample.com/abc123?referral_code=ABC123D"
}
```

### Get Referrals
> Returns a page from a collection of ReferredUsers. ReferredUser includes the user's name and their ReferrerStatus

#### Request `GET /api/users/referrals?pageSize={size}&pageStart={start}`
```
Content-Type: application/json
Authentication: Bearer <access-token-here>
```

#### RResponse `200 OK`
```
{
    "total": 120,
    "pageStart": 0,
    "pageSize": 10,
    "referredUsers": [{
        "displayName" : "Sam Doe",
        "status" : "complete"
        }, {
        "displayName" : "L. Ipsum, Esq"
        "status" : "Registered"
        },
        ...
    ]
}
```
