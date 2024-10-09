---
optionsClassName: TfsTeamSettingsEndpointOptions
optionsClassFullName: MigrationTools.Endpoints.TfsTeamSettingsEndpointOptions
configurationSamples:
- name: defaults
  order: 2
  description: 
  code: There are no defaults! Check the sample for options!
  sampleFor: MigrationTools.Endpoints.TfsTeamSettingsEndpointOptions
- name: sample
  order: 1
  description: 
  code: There is no sample, but you can check the classic below for a general feel.
  sampleFor: MigrationTools.Endpoints.TfsTeamSettingsEndpointOptions
- name: classic
  order: 3
  description: 
  code: >-
    {
      "$type": "TfsTeamSettingsEndpointOptions",
      "Collection": null,
      "Project": null,
      "Authentication": null,
      "ReflectedWorkItemIdField": null,
      "LanguageMaps": {
        "AreaPath": "Area",
        "IterationPath": "Iteration"
      }
    }
  sampleFor: MigrationTools.Endpoints.TfsTeamSettingsEndpointOptions
description: missing XML code comments
className: TfsTeamSettingsEndpoint
typeName: Endpoints
architecture: 
options:
- parameterName: Authentication
  type: TfsAuthenticationOptions
  description: missing XML code comments
  defaultValue: missing XML code comments
- parameterName: Collection
  type: Uri
  description: missing XML code comments
  defaultValue: missing XML code comments
- parameterName: LanguageMaps
  type: TfsLanguageMapOptions
  description: missing XML code comments
  defaultValue: missing XML code comments
- parameterName: Project
  type: String
  description: missing XML code comments
  defaultValue: missing XML code comments
- parameterName: ReflectedWorkItemIdField
  type: String
  description: missing XML code comments
  defaultValue: missing XML code comments
status: missing XML code comments
processingTarget: missing XML code comments
classFile: /src/MigrationTools.Clients.TfsObjectModel/EndPoints/TfsTeamSettingsEndpoint.cs
optionsClassFile: /src/MigrationTools.Clients.TfsObjectModel/EndPoints/TfsTeamSettingsEndpointOptions.cs

redirectFrom:
- /Reference/Endpoints/TfsTeamSettingsEndpointOptions/
layout: reference
toc: true
permalink: /Reference/Endpoints/TfsTeamSettingsEndpoint/
title: TfsTeamSettingsEndpoint
categories:
- Endpoints
- 
topics:
- topic: notes
  path: /docs/Reference/Endpoints/TfsTeamSettingsEndpoint-notes.md
  exists: false
  markdown: ''
- topic: introduction
  path: /docs/Reference/Endpoints/TfsTeamSettingsEndpoint-introduction.md
  exists: false
  markdown: ''

---