import { Outlet, Navigate } from "react-router-dom";

function ProtectedRoute({ usernames }) {
  const user = JSON.parse(sessionStorage.getItem("user")) || null;

  // not logged in AND page doesn't require a username
  if (!user && !usernames) {
    return <Outlet />;
  }
  // not logged in AND page requires a username
  else if (!user && usernames) {
    return <Navigate to="/login" />;
  }
  // logged in AND page doesnt't require a username
  else if (user && !usernames) {
    return <Navigate to="/" />;
  }
  // logged in AND page requires a username
  else if (user && usernames) {
    if (usernames.find((username) => username == user.username)) {
      return <Outlet />;
    } else if (user.username == "admin") {
      return <Navigate to="/admin" />;
    } else {
      return <Navigate to="/" />;
    }
  }
}

export default ProtectedRoute;
