using LiteDB;

namespace signalr_discount_code_handler
{
    public class CodeService
    {
        private readonly string _connectionString = @"Filename=./codes.db; Connection=shared";

        public async Task<bool> GenerateUniqueCodeAsync(int length)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var db = new LiteDatabase(_connectionString))
                    {
                        var codes = db.GetCollection<Code>("codes");

                        string code;
                        do
                        {
                            code = GenerateRandomCode(length);
                        }
                        while (codes.Exists(c => c.Value == code));

                        codes.Insert(new Code { Value = code });
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            });
        }

        public async Task<int> UseCode(string code)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var db = new LiteDatabase(_connectionString))
                    {
                        var codes = db.GetCollection<Code>("codes");
                        var codeToDelete = codes.FindOne(c => c.Value == code);
                        if (codeToDelete != null)
                        {
                            codes.Delete(codeToDelete.Id);
                            return StatusCodes.Status200OK;
                        }
                        else
                        {
                            Console.WriteLine("Code not found.");
                            return StatusCodes.Status400BadRequest;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return StatusCodes.Status500InternalServerError;
                }
            });
        }

        private string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public class Code
        {
            public int Id { get; set; }
            public required string Value { get; set; }
        }
    }
}