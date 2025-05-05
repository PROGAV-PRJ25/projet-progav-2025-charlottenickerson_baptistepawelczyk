public class Haricots_verts:PlanteComestible {

    public Haricots_verts(Terrain terrain) : base(terrain) {
        Nom ="Haricots verts";
        Saisons = new List<string> {"Été"};
        TerrainPrefere = "Plaine";
        Espacement = 3;
        VitesseCroissance = 2;
        BesoinsEnEau = 3;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {15, 25};
        MaladiesPossibles = new List<string> {"mildiou", "rouille"};
        ProbabilitesMaladies = new List<int> {10, 15};
        EsperanceDeVie = 1;
        Rendement = 15;
        EtatPlante ="Germination";
    }
}