# Djordje Validacija activity diagrama za dodavanje termina od strane sekretara

Prije svega želio bih  pohvaliti kolegu za dobro obavljen posao pri kreiranju dijagrama sekvenci.
Dijagram je jednostavan i lagan za čitanje, vremena izvršavanja akcija su dobro odmjerena.

Prvo sto sam primjetio jeste da nazivi na dijagramu sekvenci ne odgovaraju nazivima 
metoda u kodu. To se vjerovatno desilo  zbog toga sto jos nije radjeno refaktorisanje koda 
i sva logika se nalazi u code behind-u te je malo teze onda dati smislene nazive metoda,jer
većina logike se nalazi u nekim obradjivačima događaja.

Jos jedna stvar koja mi se ne sviđa jeste to sto se ceka da korisnik popuni sve 
podatke pa se tek onda kaže da li su one validne ili ne.Posto se podaci korisniku prezentuju
pomocu combobox-ova datapickera dobro bi bilo da se u wpf dijelu prikazuju već validni podaci.
I onda je potrebno samo uraditi neke provjere da li su sva polja popunjena.
Takođe bi se validacija mogla detaljnije opisati. Kolega bi mogao staviti neki komentar
gdje će napisati kakve je sve provjere radio.
