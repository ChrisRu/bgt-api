# API

## /api

|route|type|post|response|
|-----|----|----|--------|
| ~/authenticated   | GET  |-| `{ authorized: bool }`|
| ~/authenticate    | POST | `{ username, password }` | `{ token, expires }`
| ~/projects        | GET  |-| `[ ...{ BGTonNumber, Status, Description, Category, Location } ]`|
| ~/projects/\{id\} | GET  |-| `{ BGTonNumber, Status, Description, Category, Location }`|
| ~/users           | POST | `{ Username, Password }` | `{ Username, isAdmin }` |