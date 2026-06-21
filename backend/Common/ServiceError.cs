namespace backend.Common;

public record ServiceError(string Code, string Message, string? Field = null);
