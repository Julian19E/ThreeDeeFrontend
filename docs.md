# Dokumentation
## Abgabe
Dokumentation von
- Frameworks und Commitment auf Stylestandards, Patterns und Architektur in readme.md
- API(s): Swagger
- DB: ER Modell und relationles Modell als Bilder
- High Levl Architektur: Deploymentmodell, ggfs. mit UML
- Usecases: Kanbantickets, erweitert, formuliert in Gherkin (nach BDD)
- Konkretisierung HL Modell in Architekturmodell mit Darstellung der Entitäten, Businesslogik (Usecases), Progrmm- und Datenfluss
- Contract First: Schnittstellndefinition
- Testergebnisse: automatisiert, Output der Unit, Integration und E2E Tests als Dokument und/oder .csv

## Development:
- readme als Grundlage für Commitment
- Abhängigkeitsdiagramme, z.B. Fanin Fanout zur Überprüfung der Kopplungen und Kohäsion
- Update/autom. Generierung der Swagger Specs, insbesondere der Resourcen, Datentypen, Parameter; Beispiele!
- Verlauf und Entwicklungfortschritt: atomare Commits, klare Commitmessages, Verlinkung von Issues und Blockern
- Qualität: Reviews vor PR, Testergebnisse, Linker
- Kommentare im Code vermeiden, wenn dnn nur aus einem einzigen Grund: *Warum* ist der Code so wie er ist? Die Fragen *was* und *wie* sollte der Code selbst beantworten (Clean Code)!

## Deployment: 
- semantische Versionierung
- Documentation as Code in Pipeline yaml Files, Dockerfiles und docker compose files
- Releasenotes
- Dok der E2E Testergebnisse und Performancetests / Loadtests, z.B. als csv.

## Laufzeit: 
- Monitoring der Serverauslastungen
- Remotelogging kritischer Ereignisse (Error, Critical)
