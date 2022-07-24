# LanisterPayTPSS
A transaction payment splitting service (TPSS) meant to calculate the amount due to one or more split payment "entities" as well as the amount left after all splits have been computed.
API exposes a single HTTP POST endpoint /split-payments/compute that accepts a transaction object with the following properties:

    ID: Unique numeric ID of the transaction
    Amount: Amount to be splitted between the split entities defined in the SplitInfo array (see below)
    Currency: The currency of the transaction
    CustomerEmail: Email address of the transaction customer
    SplitInfo: An array of split entity objects. Each object conatins the fields below:
        SplitType: This defines how the split amount for the entity is calculated. It has 3 possible values, "FLAT", "PERCENTAGE" AND "RATIO"
        SplitValue: This is used together with the SplitType to determine the final value of the split amount for the entity. Example, a SplitType of FLAT and SplitValue of 45 means the split entity gets NGN 45. Another example, A SplitType of PERCENTAGE and SplitValue of 3 means the split entity gets 3 percent of the transaction amount or Balance.
        SplitEntityId: This is the unique identifier for the split entity.
Sample Payload:
`{
    "ID": 1308,
    "Amount": 12580,
    "Currency": "NGN",
    "CustomerEmail": "anon8@customers.io",
    "SplitInfo": [
        {
            "SplitType": "FLAT",
            "SplitValue": 45,
            "SplitEntityId": "LNPYACC0019"
        },
        {
            "SplitType": "RATIO",
            "SplitValue": 3,
            "SplitEntityId": "LNPYACC0011"
        },
        {
            "SplitType": "PERCENTAGE",
            "SplitValue": 3,
            "SplitEntityId": "LNPYACC0015"
        }
    ]
}`
Returns with the 200 0K HTTP code and a single object containing the following fields if request is succesful:

    ID: The unique id of the transaction. This is the same type and value as the ID value of the transaction object that was passed in the request.
    Balance: The amount left after all split values have been computed. It should always be greater than or equal to zero.
    SplitBreakdown: An array containing the breakdown of  computed split amounts for each split entity that was passed via the SplitInfo array in the request. Contains the following fields:
        SplitEntityId: The unique identifier for the split entity.
        Amount: The amount due to the split entity
        
Sample Response:
`{
    "ID": 1308,
    "Balance": 0,
    "SplitBreakdown": [
        {
            "SplitEntityId": "LNPYACC0019",
            "Amount": 5000
        },
        {
            "SplitEntityId": "LNPYACC0011",
            "Amount": 2000
        },
        {
            "SplitEntityId": "LNPYACC0015",
            "Amount": 2000
        }
    ]
}`


Business Rule:
  Each split calculation is based on the Balance after the previous calculation's done.
  The order of precedence for the SplitType is:

    FLAT types are computed before PERCENTAGE OR RATIO types
    PERCENTAGE types sare computed before RATIO types.
    RATIO types are always computed last.

  The SplitInfo array can contain a minimum of 1 split entity and a maximum of 20 entities.
  The final Balance value in your response cannot be lesser than 0.
  The split Amount value computed for each entity cannot be greater than the transaction Amount.
  The split Amount value computed for each entity cannot be lesser than 0.
  The sum of all split Amount values computed cannot be greated than the transaction Amount
