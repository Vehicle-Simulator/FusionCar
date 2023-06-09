FROM unityci/editor:ubuntu-2021.3.12f1-android-1.1.2
RUN apt-get update &&  apt-get -y install sudo
RUN mkdir actions-runner && cd actions-runner
RUN curl -o actions-runner-linux-x64-2.304.0.tar.gz -L https://github.com/actions/runner/releases/download/v2.304.0/actions-runner-linux-x64-2.304.0.tar.gz
RUN echo "292e8770bdeafca135c2c06cd5426f9dda49a775568f45fcc25cc2b576afc12f  actions-runner-linux-x64-2.304.0.tar.gz" | shasum -a 256 -c
RUN export RUNNER_ALLOW_RUNASROOT="1"
RUN tar xzf ./actions-runner-linux-x64-2.304.0.tar.gz
RUN  RUNNER_ALLOW_RUNASROOT="1" ./config.sh --url https://github.com/Vehicle-Simulator/FusionCar --token AQA333KWKQGX6GMRO5XFEQDEQL4AG
CMD  RUNNER_ALLOW_RUNASROOT="1" ./run.sh