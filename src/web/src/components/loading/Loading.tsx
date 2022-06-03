import { useState, useEffect, useRef} from 'react'
import Lottie from 'lottie-web'
import * as location from '../../../public/bird_loading.json'

function Loading() {
    const container = useRef(null);

    useEffect(() => {
      Lottie.loadAnimation({
        container: container.current,
        renderer: "svg",
        loop: false,
        autoplay: true,
        animationData: location
      });
      return () => {
        Lottie.destroy();
      };
    }, []);

    return (
      <div ref={container} className="lottie-player1" />     
    )
  }

  export default Loading