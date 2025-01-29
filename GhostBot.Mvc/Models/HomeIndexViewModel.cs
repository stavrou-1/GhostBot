using GhostBot.EntityModels;

namespace GhostBot.Mvc.Models;

public record HomeIndexViewModel(
    IList<Comment> Comments, IList<Category> Categories);