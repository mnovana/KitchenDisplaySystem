function Note({ noteText }) {
  return (
    <div>
      <div className="m-5" style={{ filter: "drop-shadow(1px 6px 3px rgba(50, 50, 0, 0.5))" }}>
        <div style={{ clipPath: "polygon(0 0, 90% 0, 100% 20%, 100% 100%, 100% 100%, 0 100%, 0 100%, 0 0)" }} className="bg-amber-200 p-1 pb-5 text-sm rounded-tr-2xl">
          NAPOMENA:
          <br />
          <br />
          <span className="font-bold">{noteText}</span>
        </div>
      </div>
    </div>
  );
}

export default Note;
