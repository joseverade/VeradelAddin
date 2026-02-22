namespace VeradelAddin.Domain.Features.DrawingExport.Policies
{
    public static class SuffixPolicy
    {

        /// <summary>
        /// [DOMAIN RULE / POLICY: ALL FILES CAN HAVE A SUFFIX]
        /// Returns the suffix based on the number of lines in the revision table
        /// </summary>
        /// <param name="RevisionTableLinesCount">Number of lines in the revision table</param>
        /// <returns>Suffix</returns>
        public static string GetSuffix(int RevisionTableLinesCount)
        {
            string suffix = string.Empty;

            if (10 > RevisionTableLinesCount && RevisionTableLinesCount > 0)
                suffix = $"_R0{RevisionTableLinesCount}";
            else
                suffix = $"_R{RevisionTableLinesCount}";

            return suffix;
        }

    }
}
