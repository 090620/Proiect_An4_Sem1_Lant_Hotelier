// Afiseaza mesajul de eroare al API-ului si, daca exista, erorile pe campuri.
export default function FormError({ error }) {
  if (!error) {
    return null;
  }

  return (
    <div className="form-error" role="alert">
      <p>{error.message}</p>
      {error.details?.length > 0 && (
        <ul>
          {error.details.map((detail) => (
            <li key={`${detail.field}-${detail.message}`}>{detail.message}</li>
          ))}
        </ul>
      )}
    </div>
  );
}
