import matplotlib.pyplot as plt
import numpy
import numpy as np
import sys

from mlagents_envs.environment import UnityEnvironment
from mlagents_envs.side_channel.engine_configuration_channel import EngineConfig, EngineConfigurationChannel

import GraphHelper

#env_name = "envs/3D_BallTest/3DBall"
#env_name = "envs/Basic_Test/Basic"
env_name = "envs/GridWorld/Api13Upgrade"

train_mode =  True

engine_configuration_channel = EngineConfigurationChannel()
env = UnityEnvironment(base_port=5006, file_name=env_name, side_channels=[engine_configuration_channel])

#Reset the environment
env.reset()

#Set the default brain to work with
group_name = env.get_agent_groups()[0]
group_spec = env.get_agent_group_spec(group_name)

# Set the time scale of the engine
engine_configuration_channel.set_configuration_parameters(width=500, height=500, time_scale=1, target_frame_rate=20)
#engine_configuration_channel.set_configuration_parameters(height=300)
#engine_configuration_channel.set_configuration_parameters(time_scale=1.0)
#engine_configuration_channel.set_configuration_parameters(target_frame_rate=10)

# Get the state of the agents
step_result = env.get_step_result(group_name)

print("Number of observations : ", len(group_spec.observation_shapes))
print("Agent state looks like: \n{}".format(step_result.obs[0][0]))

for episode in range(100):
    env.reset()
    step_result = env.get_step_result(group_name)
    done = False
    episode_rewards = 0
    while not done:
        action_size = group_spec.action_size
        if group_spec.is_action_continuous():
            action = np.random.randn(step_result.n_agents(), group_spec.action_size)
            # test for frozen graph evaluation
            obs = step_result.obs[0]
            actions = GraphHelper.get_ActionContinuous(obs)


        if group_spec.is_action_discrete():
             branch_size = group_spec.discrete_action_branches
             action = np.column_stack([np.random.randint(0, branch_size[i], size=(step_result.n_agents())) for i in range(len(branch_size))])

             # test for frozen graph evaluation
             obs = step_result.obs[0]
             # the coding of the mask is inverted in mlagents and the nn implementation
             actionMask = step_result.action_mask[0]
             invertedActionMask = np.invert(actionMask)

             actions = GraphHelper.get_ActionForGridworld(obs, invertedActionMask.astype(int))

             # the actions are some score outputs from the nn
             # the largest value is the winning action
             result = numpy.where(actions == numpy.max(actions))
             action[0] = result[1][0]


        env.set_actions(group_name, action)
        #env.set_actions(group_name, actions)
        env.step()
        step_result = env.get_step_result(group_name)
        episode_rewards += step_result.reward[0]
        done = step_result.done[0]
    print("Total reward this episode: {}".format(episode_rewards))

env.close()




