using UniquomeApp.SharedKernel.DomainCore;

namespace UniquomeApp.Domain;

public class Protein : SimpleEntity
{
    public Proteome InProteome { get; set; }
    public long InProteomeId { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Sequence { get; set; } = default!;
    public string? Gene { get; set; }
    public short ProteinExistence { get; set; }
    public short SequenceVersion { get; set; }
    public int SequenceLength { get; set; }
    public EnumProteinStatus ProteinStatus { get; set; }

    public IList<int>[] AminoacidPositions { get; }

    public Protein()
    {
        AminoacidPositions = new IList<int>[26];
        for (int i = 0; i < 26; i++)
            AminoacidPositions[i] = new List<int>();
    }

    public IList<int> PeptideLocations(string peptide)
    {
        var positions = new List<int>();
        int currentPos = 0;
        do
        {
            var position = Sequence.IndexOf(peptide, currentPos, StringComparison.Ordinal);
            if (position >= 0)
            {
                currentPos = position + 1;
                positions.Add(position);
            }
            else
                currentPos = position;
        } while (currentPos >= 0 && currentPos < Sequence.Length);

        return positions;
    }


    public IList<int> PeptideLocations(char[] peptide, int size)
    {
        var positions = new List<int>();
        int currentPos = 0;
        var p = peptide.ToString().Substring(0, size);
        do
        {
            var position = Sequence.IndexOf(p, currentPos, StringComparison.Ordinal);
            if (position >= 0)
            {
                currentPos = position + 1;
                positions.Add(position);
            }
            else
                currentPos = position;
        } while (currentPos >= 0 && currentPos < Sequence.Length);

        return positions;
    }


    public void AnalyzeAminoacidPositions()
    {
        for (int i = 0; i < 26; i++)
            AminoacidPositions[i].Clear();


        for (int i = 0; i < Sequence.Length; i++)
        {
            var index = AminoacidMapping.GetAminoacidIndex(Sequence[i]);
            AminoacidPositions[index].Add(i);
        }

        SequenceLength = Sequence.Length;
    }

    public bool CheckIfPeptideExists(string peptide)
    {
        var index = AminoacidMapping.GetAminoacidIndex(peptide[0]);
        var locations = AminoacidPositions[index];
        foreach (var location in locations)
        {
            try
            {
                if (location + peptide.Length <= SequenceLength)
                {
                    int count = 1;
                    for (int j = 1; j < peptide.Length; j++)
                    {
                        //                            if (peptide[j].Equals(Sequence[location + j]))
                        if (peptide[j] == Sequence[location + j])
                            count++;
                        else
                            break;
                    }

                    if (count == peptide.Length) return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{Code} : {location} {e.Message}");
            }
        }

        return false;
    }

    public bool CheckIfPeptideExists(char[] peptide, int size)
    {
        var index = AminoacidMapping.GetAminoacidIndex(peptide[0]);
        var locations = AminoacidPositions[index];
        foreach (var location in locations)
        {
            try
            {
                if (location + size <= SequenceLength)
                {
                    int count = 1;
                    for (int j = 1; j < size; j++)
                    {
                        //                            if (peptide[j].Equals(Sequence[location + j]))
                        if (peptide[j] == Sequence[location + j])
                            count++;
                        else
                            break;
                    }

                    if (count == size) return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{Code} : {location} {e.Message}");
            }
        }

        return false;
    }
}

public enum EnumProteinStatus
{
    Reviewed = 1,
    UnReviewed = 2
}

public static class AminoacidMapping
{
    private static readonly char[] Locations;
    static AminoacidMapping()
    {
        Locations = new char[26];
        var allLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        foreach (var letter in allLetters)
        {
            Locations[letter - 65] = letter;
        }

        //            Locations = new List<char>
        //            {
        //                'A',
        //                'R',
        //                'N',
        //                'D',
        //                'C',
        //                'E',
        //                'Q',
        //                'G',
        //                'H',
        //                'I',
        //                'L',
        //                'K',
        //                'M',
        //                'F',
        //                'P',
        //                'S',
        //                'T',
        //                'W',
        //                'Y',
        //                'V',                
        //            };
    }

    public static int GetAminoacidIndex(char aminoacid)
    {
        //            var index = Locations.IndexOf(aminoacid);
        //            var index = Locations[aminoacid - 65];
        //            if (index < 0)
        //                index = 20;

        var index = aminoacid - 65;
        if (index > 25)
            index = 0;

        return index;
    }

    public static IList<char> GetLocations()
    {
        return Locations;
    }
}