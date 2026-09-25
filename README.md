# Programmeerimine2
# Renar Siimon TA-25A

1. Role 
•	Eesmärk: Määratleb süsteemi kasutajate õigused ja rollid.
•	Väärtused:
o	USER: Tavakasutaja, kes saab teha ennustusi ja vaadata edetabeleid.
o	ADMIN: Süsteemiadministraator, kes saab hallata mänge, turniire ja kasutajaid.
2. User
•	Eesmärk: Esindab süsteemi registreeritud kasutajat.
•	Atribuudid:
o	id: int – Kasutaja unikaalne tunnus.
o	Username: String – Kasutajanimi süsteemis.
o	email: String – Kasutaja e-posti aadress.
o	password: String – Kasutaja krüpteeritud parool.
o	role: Role – Kasutajale määratud roll (USER või ADMIN).
•	Meetodid:
o	Prediction() – Seob kasutaja tema tehtud ennustustega või võimaldab ennustuste haldust.
o	Scoreboard() – Võimaldab vaadata kasutajaga seotud edetabeli andmeid.
3. Prediction
•	Eesmärk: Hoiab kasutaja tehtud ennustust konkreetse mängu tulemuse kohta.
•	Atribuudid:
o	id: int – Ennustuse unikaalne tunnus.
o	PredictionTime: DateTime – Aeg, millal kasutaja ennustuse sisestas või seda muutis.
o	HomeTeamGoals: int – Kasutaja ennustatud väravate arv kodumeeskonnale.
o	AwayTeamGoals: int – Kasutaja ennustatud väravate arv võõrsilmeeskonnale.
o	Points: int – Punktide arv, mille kasutaja selle ennustuse eest sai (arvutatakse tagantjärele).
•	Meetodid:
o	CountPoints() – Arvutab ja määrab saadud punktid, võrreldes ennustust reaalsete mängutulemustega.
o	Allowed() – Kontrollib, kas ennustuse tegemine või muutmine on ajaliselt veel lubatud (näiteks enne mängu algust).
4. Team
•	Eesmärk: Esindab turniiril osalevat võistkonda.
•	Atribuudid:
o	id: int – Meeskonna unikaalne tunnus.
o	name: String – Meeskonna nimi.
o	country: String – Riik, mida meeskond esindab.
5. Round
•	Eesmärk: Määratleb turniiri etapid ehk voorud.
•	Väärtused:
o	ROUND OF 16 – Kaheksandikfinaal.
o	QUARTER FINAL – Veerandfinaal.
o	SEMI FINAL – Poolfinaal.
o	FINAL – Finaal.
6. Game
•	Eesmärk: Esindab konkreetset jalgpalli- või spordimatši kahe meeskonna vahel.
•	Atribuudid:
o	id: int – Mängu unikaalne tunnus.
o	round: Round – Turniiri etapp, kuhu mäng kuulub (valik loendist Round).
o	StartTime: DateTime – Mängu ametlik algusaeg.
o	HomeTeamGoals: int – Kodumeeskonna löödud reaalne väravate arv.
o	AwayTeamGoals: int – Võõrsilmeeskonna löödud reaalne väravate arv.
•	Meetodid:
o	Started() – Kontrollib, kas mäng on juba alanud.
o	TeamsConfirmed() – Kinnitab, et mõlemad osalevad meeskonnad on paika pandud (vajalik edasipääsuetappides).
o	Happened() – Kontrollib või märgib mängu lõppenuks, mis lubab tulemusi lukustada.
7. Tournament
•	Eesmärk: Esindab terviklikku sporditurniiri, mis koondab endas mänge ja edetabelit.
•	Atribuudid:
o	id: int – Turniiri unikaalne tunnus.
o	name: String – Turniiri nimi (näiteks "MM 2026").
o	StartDate: Date – Turniiri alguskuupäev.
o	EndDate: Date – Turniiri lõpukuupäev.
•	Meetodid:
o	AddGame() – Lisab turniiri koosseisu uue mängu (Game).
o	LookScoreboard() – Kuvab või tagastab turniiri hetkeseisu edetabeli.
8. Scoreboard
•	Eesmärk: Haldab turniiri üldist edetabelit ja kasutajate paremusjärjestust.
•	Atribuudid:
o	id: int – Edetabeli kirje või tabeli unikaalne tunnus.
o	place: int – Kasutaja saavutatud koht edetabelis.
o	points: int – Kasutaja kogutud punktide kogusumma.
•	Meetodid:
o	update() – Uuendab edetabeli andmeid värskete mängutulemuste põhjal.
o	CalculatePosition() – Arvutab ja järjestab kasutajad punktide alusel, et määrata kõigile õige koht (place).



