az login --allow-no-subscriptions
appId=$(az ad app create --display-name "MSGraph console app" --public-client-redirect-uris "http://localhost" --query appId)
sed -i -e "s/CLIENT_ID/$appId/g" appsettings.development.json
