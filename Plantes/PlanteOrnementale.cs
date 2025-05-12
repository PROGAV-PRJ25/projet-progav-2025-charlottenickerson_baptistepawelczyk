public abstract class PlanteOrnementale:Plante {

    public PlanteOrnementale(Terrain terrain) : base(terrain) {
        EstComestible = false;
    }
}