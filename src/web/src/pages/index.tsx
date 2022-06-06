import dynamic from "next/dynamic";
import {signIn, signOut, useSession } from "next-auth/react"
import Link from "next/link";

export default function Home() {
  const MapWithNoSSR = dynamic(() => import("../components/map/Map"), {
    ssr: false,
  });

  const { data: session } = useSession()

  if(session) {
    return (
      <main>
        <div>
          <div id="map">
            <MapWithNoSSR name={session.user?.name} email={session.user?.email}/>
            Signed in as {session.user?.name} <br/>
            <button onClick={() => signOut()}>Sign out</button>
          </div>
        </div>
      </main>
  )}

  return (
    <div className="login">
      <button onClick={() => signIn()}>Sign in</button>
    </div>
  )
}