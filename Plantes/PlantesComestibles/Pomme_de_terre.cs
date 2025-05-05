public class Pomme_de_terre:PlanteComestible {

    public Pomme_de_terre(Terrain terrain) : base(terrain) {
        Nom ="Pomme de terre";
        MauvaisesHerbes = true;
        Saisons = new List<string> {"Printemps", "Été"};
        TerrainPrefere = "Plaine";
        Espacement = 5;
        VitesseCroissance = 3;
        BesoinsEnEau = 2;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {15, 20};
        MaladiesPossibles = new List<string> {"mildiou", "rhizoctone"};
        ProbabilitesMaladies = new List<int> {30, 15};
        EsperanceDeVie = 1;
        Rendement = 5;
        EtatPlante ="Germination";
    }
}