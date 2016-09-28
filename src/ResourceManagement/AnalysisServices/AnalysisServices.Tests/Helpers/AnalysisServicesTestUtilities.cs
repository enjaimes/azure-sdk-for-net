﻿// 
// Copyright (c) Microsoft.  All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using Microsoft.Azure;
using Microsoft.Azure.Management.Analysis;
using Microsoft.Azure.Management.Analysis.Models;
using Microsoft.Azure.Test.HttpRecorder;
using Microsoft.Rest;
using Microsoft.Rest.ClientRuntime.Azure.TestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.ResourceManager.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Xunit;

namespace AnalysisServices.Tests.Helpers
{
    public static class AnalysisServicesTestUtilities
    {
        public static bool IsTestTenant = false;
        private static HttpClientHandler Handler = null;
        private static string testSubscription = "00000000-0000-0000-0000-000000000000";
        private static Uri testUri = new Uri("https://api-dogfood.resources.windows-int.net/");

        // These should be filled in only if test tenant is true
#if DNX451
        private static string certName = null;
        private static string certPassword = null;
#endif
        // These are used to create default accounts
        public static string DefaultResourceGroup = "testrg";
        public static string DefaultServerName = "asaztest";
        public static string DefaultLocation = IsTestTenant ? null : "West US";
        public static ResourceSku DefaultSku = new ResourceSku
        {
            Name = SkuName.S1.ToString(),
            Tier = SkuTier.Standard.ToString()
        };

        public static Dictionary<string, string> DefaultTags = new Dictionary<string, string>
            {
                {"key1","value1"},
                {"key2","value2"}
            };

        public static IList<string> DefaultAdministrators = new List<string>()
        {
            "aztest0@contoso.com",
            "aztest1@contoso.com"
        };

        public static string GetDefaultCreatedResponse(string provisioningState)
        {
            string responseFormat = @"{{
                            'id':'/subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.AnalysisServices/servers/{2}',
                            'name':'{2}',
                            'type':'Microsoft.AnalysisServices/servers',
                            'location':'{3}',
                            'sku':{{
                                'name':'{4}'
                            }},
                            'tags':{5},
                            'properties':{{
                                'provisioningState':'{6}',
                                'serverFullName':'asazure://wcus.asazure-int.windows.net/{2}',
                                'asAdministrators':{{
                                'members':{7}
                                }}
                            }}
                            }}";
            
            var tags = JsonConvert.SerializeObject(DefaultTags, new KeyValuePairConverter());
            var admins = JsonConvert.SerializeObject(DefaultAdministrators);
            return string.Format(
                responseFormat,
                testSubscription,
                DefaultResourceGroup,
                DefaultServerName,
                DefaultLocation,
                DefaultSku.Name,
                tags,
                provisioningState,
                admins);
        }

        public static ResourceManagementClient GetResourceManagementClient(MockContext context, RecordedDelegatingHandler handler)
        {
            if (IsTestTenant)
            {
                return null;
            }
            else
            {
                handler.IsPassThrough = true;
                ResourceManagementClient resourcesClient = context.GetServiceClient<ResourceManagementClient>(handlers: handler);
                return resourcesClient;
            }
        }

        public static AnalysisServicesResource GetDefaultAnalysisServicesResource()
        {
            AnalysisServicesResource defaultServer = new AnalysisServicesResource
            {
                Location = DefaultLocation,
                Tags = DefaultTags,
                Sku = DefaultSku,
                AsAdministrators = new ServerAdministrators(DefaultAdministrators)
            };

            return defaultServer;
        }

        public static AnalysisServicesClient GetAnalysisServicesClient(RecordedDelegatingHandler handler)
        {
            handler.IsPassThrough = false;
            var client = new AnalysisServicesClient(new TokenCredentials("xyz"), handler);
            client.SubscriptionId = "00000000-0000-0000-0000-000000000000";
            if (HttpMockServer.Mode != HttpRecorderMode.Record)
            {
                client.LongRunningOperationRetryTimeout = 0;
            }
            return client;
        }

        public static AnalysisServicesClient GetAnalysisServicesClient(MockContext context, RecordedDelegatingHandler handler)
        {
            AnalysisServicesClient analysisServicesClient;
            if (IsTestTenant)
            {
                analysisServicesClient = new AnalysisServicesClient(new TokenCredentials("xyz"), GetHandler());
                analysisServicesClient.SubscriptionId = testSubscription;
                analysisServicesClient.BaseUri = testUri;
            }
            else
            {
                handler.IsPassThrough = true;
                analysisServicesClient = context.GetServiceClient<AnalysisServicesClient>(handlers: handler);
            }

            return analysisServicesClient;
        }

        private static HttpClientHandler GetHandler()
        {
#if DNX451
            if (Handler == null)
            {
                //talk to yugangw-msft, if the code doesn't work under dnx451 (same with net451)
                X509Certificate2 cert = new X509Certificate2(certName, certPassword);
                Handler = new System.Net.Http.WebRequestHandler();
                ((WebRequestHandler)Handler).ClientCertificates.Add(cert);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => { return true; };
            }
#endif
            return Handler;
        }

        public static string CreateResourceGroup(ResourceManagementClient resourcesClient)
        {
            const string testPrefix = "res";
            var rgname = TestUtilities.GenerateName(testPrefix);

            if (!IsTestTenant)
            {
                var resourceGroup = resourcesClient.ResourceGroups.CreateOrUpdate(
                    rgname,
                    new ResourceGroup
                    {
                        Location = DefaultLocation
                    });
            }

            return rgname;
        }

        public static void DeleteResourceGroup(ResourceManagementClient resourcesClient, string resourceGroupName)
        {
            if (!IsTestTenant)
            {
                resourcesClient.ResourceGroups.Delete(resourceGroupName);
            }
        }

        public static void WaitIfNotInPlaybackMode()
        {
            if (Environment.GetEnvironmentVariable("AZURE_TEST_MODE") != null &&
                !Environment.GetEnvironmentVariable("AZURE_TEST_MODE").
                Equals("Playback", StringComparison.CurrentCultureIgnoreCase))
            {
                Thread.Sleep(TimeSpan.FromMinutes(1));
            }
        }
    }
}
