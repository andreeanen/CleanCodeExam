# CleanCodeExam
## Andreea Nenciu

* Vad du valt att testa och varför?

Tyvärr har jah bara hunnit testa GetOrderedMovies() metoden eftersom jag inte hann med mer. Jag hade jättegärna velat testa me, flera metoder.
Jag hade behövd andra tester med mock av filmlistor och testat också metoderna i MovieController.

* Vilket/vilka designmönster har du valt, varför? Hade det gått att göra på ett annat sätt?

Jag valde att använda visitor design pattern för att separera logiken från order-modellen. Detta gör det möjligt att skapa en datamodell med begränsad intern funktionalitet och sätter upp en visitor som utför operationerna på data. Och för att jag var extrem tidspresat och det kändes en lätt deign mönster att implmentera.

* Hur mycket valde du att optimera koden, varför är det en rimlig nivå för vårt program?

Det blev många ändringar på namngivning, på att separera metoder så att det blir läsbart ocuh att det ska faktiskt göra vad den skulle.
