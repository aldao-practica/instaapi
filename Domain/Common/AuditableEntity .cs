using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Common
{
    public abstract class AuditableEntity : BaseEntity
    // ¿Por qué es importante?
    //En una aplicación real necesitás rastrear cambios:

    //Un usuario elimina un post → querés saber quién lo eliminó
    //Alguien edita un comentario ofensivo → necesitás el historial de quién y cuándo
    //Para cumplir regulaciones(GDPR, auditorías de seguridad)
    //Para debug: "¿quién cambió esta configuración?"

    //¿Qué entidades deberían heredar de AuditableEntity?

    //User ✅ - querés saber cuándo se modificó el perfil
    //Post ✅ - importante para contenido reportado
    //Comment ✅ - crítico para moderación
    //Like ❌ - no necesita auditoría, es una acción simple
    //Follow ❌ - tampoco, es binario(seguís o no seguís)

    //Por eso Like y Follow heredan solo de BaseEntity(que tiene Id y CreatedAt), mientras que User, Post y Comment heredan de AuditableEntity.
    //¿Cómo se llena automáticamente?
    //Más adelante, cuando configuremos Entity Framework, vamos a hacer que se complete automáticamente en el SaveChanges:
    {
        public string CreatedBy { get; protected set; } = string.Empty;
        public string? LastModifiedBy { get; protected set; }

        protected void SetAudit(string userId)
        {
            LastModifiedBy = userId;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
