---
optionsClassName: OutboundLinkCheckingProcessorOptions
optionsClassFullName: MigrationTools.Clients.AzureDevops.Rest.Processors.OutboundLinkCheckingProcessorOptions
configurationSamples:
- name: defaults
  order: 2
  description: 
  code: There are no defaults! Check the sample for options!
  sampleFor: MigrationTools.Clients.AzureDevops.Rest.Processors.OutboundLinkCheckingProcessorOptions
- name: sample
  order: 1
  description: 
  code: There is no sample, but you can check the classic below for a general feel.
  sampleFor: MigrationTools.Clients.AzureDevops.Rest.Processors.OutboundLinkCheckingProcessorOptions
- name: classic
  order: 3
  description: 
  code: >-
    {
      "$type": "OutboundLinkCheckingProcessorOptions",
      "Enabled": false,
      "WIQLQuery": null,
      "ResultFileName": null,
      "SourceName": null,
      "TargetName": null
    }
  sampleFor: MigrationTools.Clients.AzureDevops.Rest.Processors.OutboundLinkCheckingProcessorOptions
description: missing XML code comments
className: OutboundLinkCheckingProcessor
typeName: Processors
architecture: 
options:
- parameterName: Enabled
  type: Boolean
  description: If set to `true` then the processor will run. Set to `false` and the processor will not run.
  defaultValue: missing XML code comments
- parameterName: ResultFileName
  type: String
  description: missing XML code comments
  defaultValue: missing XML code comments
- parameterName: SourceName
  type: String
  description: missing XML code comments
  defaultValue: missing XML code comments
- parameterName: TargetName
  type: String
  description: missing XML code comments
  defaultValue: missing XML code comments
- parameterName: WIQLQuery
  type: String
  description: missing XML code comments
  defaultValue: missing XML code comments
status: missing XML code comments
processingTarget: missing XML code comments
classFile: /src/MigrationTools.Clients.AzureDevops.Rest/Processors/OutboundLinkCheckingProcessor.cs
optionsClassFile: /src/MigrationTools.Clients.AzureDevops.Rest/Processors/OutboundLinkCheckingProcessorOptions.cs

redirectFrom:
- /Reference/Processors/OutboundLinkCheckingProcessorOptions/
layout: reference
toc: true
permalink: /Reference/Processors/OutboundLinkCheckingProcessor/
title: OutboundLinkCheckingProcessor
categories:
- Processors
- 
topics:
- topic: notes
  path: /docs/Reference/Processors/OutboundLinkCheckingProcessor-notes.md
  exists: false
  markdown: ''
- topic: introduction
  path: /docs/Reference/Processors/OutboundLinkCheckingProcessor-introduction.md
  exists: false
  markdown: ''

---