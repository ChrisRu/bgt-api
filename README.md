# Setup

1. Run `git clone https://github.com/ChrisRu/bgt-api`
2. Open file manager
3. In address bar enter `%appdata%`
4. Go to `Microsoft/UserSecrets` (make the folder if it doesn't exist)
5. Create a folder named `1dd703cb-7192-4313-9000-7df579dcb5b5`
6. Create a `secrets.json` inside that folder
7. Edit the `secrets.json` with:
```json
{
  "Authentication": {
    "Key": "temporary_super_secret_key_extra_secure_secret_hidden_not_displayed",
    "Issuer": "TestUser",
    "Audience": "TestAudience"
  },
  "Database": {
    // EDIT THIS WITH THE CORRECT SHIT
    "ConnectionString": "Server=url;Database=bgt;User Id=username;Password=password;"
  }
}
```
8. Open the solution in Visual Studio
9. Run the program and pray