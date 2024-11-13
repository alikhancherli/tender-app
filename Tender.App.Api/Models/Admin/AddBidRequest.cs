namespace Tender.App.Api.Models.Admin;

public record AddBidRequest(
    string Title,
    string Description,
    DateTime StartIn,
    DateTime EndIn,
    long MinAmount,
    long MaxAmount);
