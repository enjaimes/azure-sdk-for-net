// 
// Copyright (c) Microsoft and contributors.  All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// 
// See the License for the specific language governing permissions and
// limitations under the License.
// 

// Warning: This code was generated by a tool.
// 
// Changes to this file may cause incorrect behavior and will be lost if the
// code is regenerated.

using System;
using System.Linq;
using System.Net.Http;
using Hyak.Common;
using Microsoft.Azure;
using Microsoft.Azure.Management.Insights;

namespace Microsoft.Azure.Management.Insights
{
    public partial class InsightsManagementClient : ServiceClient<InsightsManagementClient>, IInsightsManagementClient
    {
        private string _apiVersion;
        
        /// <summary>
        /// Gets the API version.
        /// </summary>
        public string ApiVersion
        {
            get { return this._apiVersion; }
        }
        
        private Uri _baseUri;
        
        /// <summary>
        /// Gets the URI used as the base for all cloud service requests.
        /// </summary>
        public Uri BaseUri
        {
            get { return this._baseUri; }
        }
        
        private SubscriptionCloudCredentials _credentials;
        
        /// <summary>
        /// Gets subscription credentials which uniquely identify Microsoft
        /// Azure subscription. The subscription ID forms part of the URI for
        /// every service call.
        /// </summary>
        public SubscriptionCloudCredentials Credentials
        {
            get { return this._credentials; }
        }
        
        private int _longRunningOperationInitialTimeout;
        
        /// <summary>
        /// Gets or sets the initial timeout for Long Running Operations.
        /// </summary>
        public int LongRunningOperationInitialTimeout
        {
            get { return this._longRunningOperationInitialTimeout; }
            set { this._longRunningOperationInitialTimeout = value; }
        }
        
        private int _longRunningOperationRetryTimeout;
        
        /// <summary>
        /// Gets or sets the retry timeout for Long Running Operations.
        /// </summary>
        public int LongRunningOperationRetryTimeout
        {
            get { return this._longRunningOperationRetryTimeout; }
            set { this._longRunningOperationRetryTimeout = value; }
        }
        
        private IAgentDiagnosticSettingsOperations _agentDiagnosticSettingsOperations;
        
        /// <summary>
        /// Operations for managing agent diagnostic settings.
        /// </summary>
        public virtual IAgentDiagnosticSettingsOperations AgentDiagnosticSettingsOperations
        {
            get { return this._agentDiagnosticSettingsOperations; }
        }
        
        private IAlertOperations _alertOperations;
        
        /// <summary>
        /// Operations for managing alert rules and incidents.
        /// </summary>
        public virtual IAlertOperations AlertOperations
        {
            get { return this._alertOperations; }
        }
        
        private IAutoscaleOperations _autoscaleOperations;
        
        /// <summary>
        /// Operations for managing autoscale.
        /// </summary>
        public virtual IAutoscaleOperations AutoscaleOperations
        {
            get { return this._autoscaleOperations; }
        }
        
        private IMonitoringConfigurationOperations _monitoringConfigurationOperations;
        
        /// <summary>
        /// Operations for managing the collection of diagonstics and
        /// monitoring data.
        /// </summary>
        public virtual IMonitoringConfigurationOperations MonitoringConfigurationOperations
        {
            get { return this._monitoringConfigurationOperations; }
        }
        
        private IServiceDiagnosticSettingsOperations _serviceDiagnosticSettingsOperations;
        
        /// <summary>
        /// Operations for managing service diagnostic settings.
        /// </summary>
        public virtual IServiceDiagnosticSettingsOperations ServiceDiagnosticSettingsOperations
        {
            get { return this._serviceDiagnosticSettingsOperations; }
        }
        
        private ISkuOperations _skuOperations;
        
        /// <summary>
        /// Operations for managing resources sku.
        /// </summary>
        public virtual ISkuOperations SkuOperations
        {
            get { return this._skuOperations; }
        }
        
        private IStorageDiagnosticSettingsOperations _storageDiagnosticSettingsOperations;
        
        /// <summary>
        /// Operations for managing storage diagnostic settings.
        /// </summary>
        public virtual IStorageDiagnosticSettingsOperations StorageDiagnosticSettingsOperations
        {
            get { return this._storageDiagnosticSettingsOperations; }
        }
        
        /// <summary>
        /// Initializes a new instance of the InsightsManagementClient class.
        /// </summary>
        public InsightsManagementClient()
            : base()
        {
            this._agentDiagnosticSettingsOperations = new AgentDiagnosticSettingsOperations(this);
            this._alertOperations = new AlertOperations(this);
            this._autoscaleOperations = new AutoscaleOperations(this);
            this._monitoringConfigurationOperations = new MonitoringConfigurationOperations(this);
            this._serviceDiagnosticSettingsOperations = new ServiceDiagnosticSettingsOperations(this);
            this._skuOperations = new SkuOperations(this);
            this._storageDiagnosticSettingsOperations = new StorageDiagnosticSettingsOperations(this);
            this._apiVersion = "2014-04-01";
            this._longRunningOperationInitialTimeout = -1;
            this._longRunningOperationRetryTimeout = -1;
            this.HttpClient.Timeout = TimeSpan.FromSeconds(300);
        }
        
        /// <summary>
        /// Initializes a new instance of the InsightsManagementClient class.
        /// </summary>
        /// <param name='credentials'>
        /// Required. Gets subscription credentials which uniquely identify
        /// Microsoft Azure subscription. The subscription ID forms part of
        /// the URI for every service call.
        /// </param>
        /// <param name='baseUri'>
        /// Optional. Gets the URI used as the base for all cloud service
        /// requests.
        /// </param>
        public InsightsManagementClient(SubscriptionCloudCredentials credentials, Uri baseUri)
            : this()
        {
            if (credentials == null)
            {
                throw new ArgumentNullException("credentials");
            }
            if (baseUri == null)
            {
                throw new ArgumentNullException("baseUri");
            }
            this._credentials = credentials;
            this._baseUri = baseUri;
            
            this.Credentials.InitializeServiceClient(this);
        }
        
        /// <summary>
        /// Initializes a new instance of the InsightsManagementClient class.
        /// </summary>
        /// <param name='credentials'>
        /// Required. Gets subscription credentials which uniquely identify
        /// Microsoft Azure subscription. The subscription ID forms part of
        /// the URI for every service call.
        /// </param>
        public InsightsManagementClient(SubscriptionCloudCredentials credentials)
            : this()
        {
            if (credentials == null)
            {
                throw new ArgumentNullException("credentials");
            }
            this._credentials = credentials;
            this._baseUri = new Uri("https://management.azure.com");
            
            this.Credentials.InitializeServiceClient(this);
        }
        
        /// <summary>
        /// Initializes a new instance of the InsightsManagementClient class.
        /// </summary>
        /// <param name='httpClient'>
        /// The Http client
        /// </param>
        public InsightsManagementClient(HttpClient httpClient)
            : base(httpClient)
        {
            this._agentDiagnosticSettingsOperations = new AgentDiagnosticSettingsOperations(this);
            this._alertOperations = new AlertOperations(this);
            this._autoscaleOperations = new AutoscaleOperations(this);
            this._monitoringConfigurationOperations = new MonitoringConfigurationOperations(this);
            this._serviceDiagnosticSettingsOperations = new ServiceDiagnosticSettingsOperations(this);
            this._skuOperations = new SkuOperations(this);
            this._storageDiagnosticSettingsOperations = new StorageDiagnosticSettingsOperations(this);
            this._apiVersion = "2014-04-01";
            this._longRunningOperationInitialTimeout = -1;
            this._longRunningOperationRetryTimeout = -1;
            this.HttpClient.Timeout = TimeSpan.FromSeconds(300);
        }
        
        /// <summary>
        /// Initializes a new instance of the InsightsManagementClient class.
        /// </summary>
        /// <param name='credentials'>
        /// Required. Gets subscription credentials which uniquely identify
        /// Microsoft Azure subscription. The subscription ID forms part of
        /// the URI for every service call.
        /// </param>
        /// <param name='baseUri'>
        /// Optional. Gets the URI used as the base for all cloud service
        /// requests.
        /// </param>
        /// <param name='httpClient'>
        /// The Http client
        /// </param>
        public InsightsManagementClient(SubscriptionCloudCredentials credentials, Uri baseUri, HttpClient httpClient)
            : this(httpClient)
        {
            if (credentials == null)
            {
                throw new ArgumentNullException("credentials");
            }
            if (baseUri == null)
            {
                throw new ArgumentNullException("baseUri");
            }
            this._credentials = credentials;
            this._baseUri = baseUri;
            
            this.Credentials.InitializeServiceClient(this);
        }
        
        /// <summary>
        /// Initializes a new instance of the InsightsManagementClient class.
        /// </summary>
        /// <param name='credentials'>
        /// Required. Gets subscription credentials which uniquely identify
        /// Microsoft Azure subscription. The subscription ID forms part of
        /// the URI for every service call.
        /// </param>
        /// <param name='httpClient'>
        /// The Http client
        /// </param>
        public InsightsManagementClient(SubscriptionCloudCredentials credentials, HttpClient httpClient)
            : this(httpClient)
        {
            if (credentials == null)
            {
                throw new ArgumentNullException("credentials");
            }
            this._credentials = credentials;
            this._baseUri = new Uri("https://management.azure.com");
            
            this.Credentials.InitializeServiceClient(this);
        }
        
        /// <summary>
        /// Clones properties from current instance to another
        /// InsightsManagementClient instance
        /// </summary>
        /// <param name='client'>
        /// Instance of InsightsManagementClient to clone to
        /// </param>
        protected override void Clone(ServiceClient<InsightsManagementClient> client)
        {
            base.Clone(client);
            
            if (client is InsightsManagementClient)
            {
                InsightsManagementClient clonedClient = ((InsightsManagementClient)client);
                
                clonedClient._credentials = this._credentials;
                clonedClient._baseUri = this._baseUri;
                clonedClient._apiVersion = this._apiVersion;
                clonedClient._longRunningOperationInitialTimeout = this._longRunningOperationInitialTimeout;
                clonedClient._longRunningOperationRetryTimeout = this._longRunningOperationRetryTimeout;
                
                clonedClient.Credentials.InitializeServiceClient(clonedClient);
            }
        }
    }
}
