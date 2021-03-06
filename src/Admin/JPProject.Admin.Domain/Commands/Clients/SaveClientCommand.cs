using IdentityServer4.Models;
using JPProject.Admin.Domain.Validations.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using JPProject.Domain.Core.Util;

namespace JPProject.Admin.Domain.Commands.Clients
{
    public class SaveClientCommand : ClientCommand
    {
        public ClientType ClientType { get; }

        public SaveClientCommand(string clientId, string name, string clientUri, string logoUri, string description,
            ClientType clientType, string postLogoutUri)
        {
            Client = new Client()
            {
                ClientId = clientId?.Trim(),
                ClientName = name?.Trim(),
                ClientUri = clientUri?.Trim(),
                LogoUri = logoUri?.Trim(),
                Description = description?.Trim(),
            };

            if (postLogoutUri.IsPresent())
                Client.PostLogoutRedirectUris = new List<string>() { postLogoutUri.Trim() };
            ClientType = clientType;
        }

        public Client ToModel()
        {
            PrepareClientTypeForNewClient();
            return Client;
        }

        private void PrepareClientTypeForNewClient()
        {
            switch (ClientType)
            {
                case ClientType.Empty:
                    break;
                case ClientType.Device:
                    ConfigureDevice();
                    break;
                case ClientType.WebServerSideRenderer:
                    ConfigureWebServerSide();
                    break;
                case ClientType.Spa:
                    ConfigureWebSpa();
                    break;
                case ClientType.WebHybrid:
                    ConfigureWebHybrid();
                    break;
                case ClientType.Native:
                    ConfigureNative();
                    break;
                case ClientType.Machine:
                    ConfigureMachine();

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ClientType));
            }

        }

        private void ConfigureDefaultUrls()
        {
            if (Client.ClientUri.IsPresent())
            {
                Client.AllowedCorsOrigins.Add(Client.ClientUri);
                Client.RedirectUris.Add(Client.ClientUri);

                if (!Client.PostLogoutRedirectUris.Any())
                    Client.PostLogoutRedirectUris.Add(Client.ClientUri);
            }

        }

        public override bool IsValid()
        {
            ValidationResult = new SaveClientCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        private void ConfigureDevice()
        {
            Client.AllowedGrantTypes.Add(GrantType.DeviceFlow);
            Client.AllowedScopes.Add("openid");

            Client.RequireClientSecret = false;
            Client.AllowOfflineAccess = true;

        }

        private void ConfigureWebServerSide()
        {
            Client.AllowedGrantTypes.Add(GrantType.AuthorizationCode);
            Client.AllowAccessTokensViaBrowser = false;
            Client.RequireClientSecret = true;
            Client.RequirePkce = true;
            Client.AllowedScopes.Add("openid");
            Client.AllowedScopes.Add("profile");
            ConfigureDefaultUrls();

        }
        private void ConfigureWebSpa()
        {
            Client.AllowedGrantTypes.Add(GrantType.AuthorizationCode);
            Client.RequireClientSecret = false;
            Client.RequirePkce = true;

            Client.AllowedScopes.Add("openid");
            Client.AllowedScopes.Add("profile");
            ConfigureDefaultUrls();

        }

        private void ConfigureWebHybrid()
        {
            Client.AllowedGrantTypes.Add(GrantType.Hybrid);
            Client.AllowedScopes.Add("openid");
            Client.AllowedScopes.Add("profile");
            ConfigureDefaultUrls();

        }

        private void ConfigureNative()
        {
            Client.AllowedGrantTypes.Add(GrantType.AuthorizationCode);
            Client.RequireClientSecret = false;
            Client.RequirePkce = true;
            Client.AllowedScopes.Add("openid");
            Client.AllowedScopes.Add("profile");
        }

        private void ConfigureMachine()
        {
            Client.AllowedGrantTypes.Add(GrantType.ClientCredentials);
            Client.AllowedScopes.Add("openid");
            Client.RequireConsent = false;
            Client.RequireClientSecret = true;
        }

    }
}