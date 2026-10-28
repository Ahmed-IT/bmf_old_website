param(
    [uri]$BaseUri = 'https://bmf.com.bh/',
    [ValidateRange(1, 12)]
    [int]$Attempts = 1
)

$ErrorActionPreference = 'Stop'

for ($attempt = 1; $attempt -le $Attempts; $attempt++) {
    try {
        foreach ($path in @('', 'AboutUs', 'Services/CompetitionLicense', 'Home/PrivacyPolicy')) {
            $uri = [uri]::new($BaseUri, $path)
            $response = Invoke-WebRequest -UseBasicParsing -Uri $uri -MaximumRedirection 0 -TimeoutSec 90
            if ($response.StatusCode -ne 200 -or
                $response.Content -notmatch '<title>\s*Bahrain Motor Federation\s*</title>' -or
                $response.Content -notmatch '/Content/images/BMF_Logo.png') {
                throw "The public website did not return its expected page at $uri."
            }
            Write-Output "Public page verified: $uri"
        }

        foreach ($path in @(
            'Content/images/BMF_Logo.png',
            'Content/css/NewStyle.css',
            'Content/bootstrap5/css/bootstrap.css',
            'Content/fonts/fontawesome-webfont.ttf'
        )) {
            $uri = [uri]::new($BaseUri, $path)
            $response = Invoke-WebRequest -UseBasicParsing -Uri $uri -MaximumRedirection 0 -TimeoutSec 90
            if ($response.StatusCode -ne 200 -or $response.RawContentLength -eq 0 -or
                $response.Headers['Content-Type'] -match 'text/html') {
                throw "The public website asset is missing or redirected: $uri."
            }
            Write-Output "Public asset verified: $uri"
        }
        break
    }
    catch {
        if ($attempt -eq $Attempts) { throw }
        Write-Warning "Public-site check $attempt of $Attempts failed: $($_.Exception.Message). Retrying after deployment warmup."
        Start-Sleep -Seconds 15
    }
}
