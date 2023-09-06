using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;

namespace Controllers;

public class MyControllerFeatureProvider : ControllerFeatureProvider
{
    protected override bool IsController(TypeInfo typeInfo)
    {
        var isController = base.IsController(typeInfo);

        if (!isController)
        {
            string[] validEndings = new[] { "Foobar", "BaseContentController`1" };

            isController = validEndings.Any(x =>
                typeInfo.Name.EndsWith(x, StringComparison.OrdinalIgnoreCase));
        }

        Console.WriteLine($"{typeInfo.Name} IsController: {isController}.");

        return isController;
    }
}