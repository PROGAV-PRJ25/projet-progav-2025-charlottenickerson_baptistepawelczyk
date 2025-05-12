public class Lotus_bleu:PlanteOrnementale {

    public Lotus_bleu(Terrain terrain) : base(terrain) {
        Nom ="Lotus bleu";
        Type = "Vivace";
        Saisons = new List<string> {"Été"};
        TerrainPrefere = "Marais";
        Espacement = 4;
        VitesseCroissance = 0.233;
        BesoinsEnEau = 8;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {20, 35};
        MaladiesPossibles = new List<string> {"pourriture des racines", "pucerons aquatiques"};
        ProbabilitesMaladies = new List<int> {10, 15};
        EsperanceDeVie = 1;
        Rendement = 5;
        EtatPlante ="Germination";
    }
}