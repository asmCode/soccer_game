public struct PlayerId
{
    public byte Team { get; set; }
    public byte Index { get; set; }

    public PlayerId(byte team, byte index)
    {
        Team = team;
        Index = index;
    }

    public override bool Equals(object obj)
    {
        if (!(obj is PlayerId))
        {
            return false;
        }

        var id = (PlayerId)obj;
        return Team == id.Team &&
               Index == id.Index;
    }

    public override int GetHashCode()
    {
        var hashCode = 1474331223;
        hashCode = hashCode * -1521134295 + Team.GetHashCode();
        hashCode = hashCode * -1521134295 + Index.GetHashCode();
        return hashCode;
    }

    public static bool operator ==(PlayerId left, PlayerId right)
    {
        return left.Team == right.Team && left.Index == right.Index;
    }

    public static bool operator !=(PlayerId left, PlayerId right)
    {
        return left.Team != right.Team || left.Index != right.Index;
    }
}
