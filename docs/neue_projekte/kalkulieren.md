# Preisangaben

Möchten Sie Preise hinzufügen, haben Sie zwei Möglichkeiten:
* Sie können einfach direkt in der jeweiligen Positionen einen Einheitspreis und eine Menge definieren.
* Im Blatt `Kalkulation` haben Sie die Möglichkeit, eine detaillierte Preiskalkulation anzulegen.

Gesamtpreisen für Gruppen berechnen sich aus der Summe der Gesamtpreise aller untergeordneten Positionen und Gruppen. Das bedeutet, Positionen, die nur einen Einheitspreis aber keine _Menge_ definiert haben, tragen nicht zum Gesamtpreis der übergeordneten Elemente bei.

[Hier erfahren Sie mehr über das Kalkulieren in Excel](https://www.dangl-it.de/artikel/arbeiten-mit-gaeb-in-microsoft-excel/).

> **Achtung**: Beachten Sie bitte, dass das Kalkulieren im Kalkulationsblatt nicht möglich ist für neue, von Ihnen hinzugefügte Elemente. Um das zu umgehen müssen Sie Ihr Arbeitsblatt mit Web**GAEB** einmal konvertieren. Das funktioniert auch direkt von Excel zu Excel.
> Der Grund hierfür ist, dass Ordnungszahlen nicht zwingend eindeutig sind, und somit kann keine direkte Referenz zwischen dem Kalkulationsblatt und den Positionen im Projekt hergestellt werden. Wird das Projekt jedoch durch Web**GAEB** verarbeitet, erhält jede Position eine eindeutige Id, die in einer versteckten Spalte gespeichert wird.
