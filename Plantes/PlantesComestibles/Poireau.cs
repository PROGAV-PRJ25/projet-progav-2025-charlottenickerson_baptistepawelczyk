public class Poireau:PlanteComestible {

    public Poireau(Terrain terrain) : base(terrain) {
        Nom ="Poireau";
        Type = "Vivace";
        Saisons = new List<string> {"Automne", "Hiver"};
        TerrainPrefere = "Plaine";
        Espacement = 3;
        VitesseCroissance = 4;
        BesoinsEnEau = 3;
        BesoinsEnLuminosite = "Luminosité moyenne";
        TemperaturesPreferees = new List<int> {10, 20};
        MaladiesPossibles = new List<string> {"mildiou", "pourriture du collet"};
        ProbabilitesMaladies = new List<int> {10, 15};
        EsperanceDeVie = 2;
        Rendement = 1;
        EtatPlante ="Germination";
    }
}