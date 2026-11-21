export function Loading({ label = 'Se încarcă...' }) {
  return <p className="state muted">{label}</p>;
}

export function EmptyState({ children }) {
  return <div className="state empty">{children}</div>;
}
