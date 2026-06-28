namespace LocalHiringPlatform.Infrastructure.EmailTemplates
{
    public static class VerificationEmailTemplate
    {
        public static string Build(string verificationUrl)
        {
            return $@"
<!DOCTYPE html>
<html>
<body style='font-family: Arial, Helvetica, sans-serif;'>

<h2>Welcome to LocalHire AI</h2>

<p>
Thank you for registering.
</p>

<p>
Please verify your email address by clicking the link below.
</p>

<p>
<a href='{verificationUrl}'>
Verify Email
</a>
</p>

<p>
If you did not create this account, you can safely ignore this email.
</p>

</body>
</html>";
        }
    }
}