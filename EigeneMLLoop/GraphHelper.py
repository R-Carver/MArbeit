import tensorflow as tf
import numpy as np
from distutils.version import LooseVersion

# LooseVersion handles things "1.2.3a" or "4.5.6-rc7" fairly sensibly.
_is_tensorflow2 = LooseVersion(tf.__version__) >= LooseVersion("2.0.0")

if _is_tensorflow2:
    import tensorflow.compat.v1 as tf

    tf.disable_v2_behavior()



def load_pb(path_to_pb):
    with tf.gfile.GFile(path_to_pb, "rb") as f:
        graph_def = tf.GraphDef()
        graph_def.ParseFromString(f.read())
    with tf.Graph().as_default() as graph:
        tf.import_graph_def(graph_def, name='')

    #with tf.Session(graph=graph) as sess:
    #    writer = tf.summary.FileWriter('./TestGraphs', sess.graph)

    return graph

def get_ActionContinuous(observations):

    # get the trained graph from the pb file
    PATH = './Graphs/frozen_graph_def.pb'
    graph = load_pb(PATH)

    # get the tensors for the Fetch and FeedDict Paramters
    fetchTensor = graph.get_tensor_by_name('action:0')

    obsTensor = graph.get_tensor_by_name('vector_observation:0')
    epsilonTensor = graph.get_tensor_by_name('epsilon:0')

    # create the feed dict with the observatrions from the mlAgents env
    feed_dict = {}
    feed_dict[obsTensor] = observations
    feed_dict[epsilonTensor] = np.zeros((12, 2), dtype=int)

    sess = tf.Session(graph=graph)

    return sess.run(fetchTensor, feed_dict=feed_dict)

def get_ActionForBasic(observations):

    # get the trained graph from the pb file
    PATH = './Graphs/Basic/frozen_graph_def.pb'
    graph = load_pb(PATH)

    # get the tensors for the Fetch and FeedDict Paramters
    fetchTensor = graph.get_tensor_by_name('action:0')
    #fetchTensor = graph.get_tensor_by_name('Identity')

    obsTensor = graph.get_tensor_by_name('vector_observation:0')

    actionMaskTensor = graph.get_tensor_by_name('action_masks:0')

    # create the feed dict with the observatrions from the mlAgents env
    feed_dict = {}
    feed_dict[obsTensor] = observations
    #feed_dict[actionMaskTensor] = np.zeros((1, 3), dtype=int)
    feed_dict[actionMaskTensor] = np.ones((1, 3), dtype=int)

    sess = tf.Session(graph=graph)

    return sess.run(fetchTensor, feed_dict=feed_dict)

def get_ActionForGridworld(observations, actionMaskArray):

    # get the trained graph from the pb file
    PATH = './Graphs/Gridworld/frozen_graph_def.pb'
    graph = load_pb(PATH)

    # get the tensors for the Fetch and FeedDict Paramters
    fetchTensor = graph.get_tensor_by_name('action:0')
    #fetchTensor = graph.get_tensor_by_name('Identity')

    obsTensor = graph.get_tensor_by_name('visual_observation_0:0')
    actionMaskTensor = graph.get_tensor_by_name('action_masks:0')

    # create the feed dict with the observatrions from the mlAgents env
    feed_dict = {}
    feed_dict[obsTensor] = observations
    #feed_dict[actionMaskTensor] = actionMaskArray
    testArray = np.ones((1, 5), dtype=int)

    # the nn expects a action mask with 1 for enabled and 0 for disabled
    # the ml agent mask codes it the other way round with false meaning not blocked -> enabled

    testArray[0][0] = 1
    testArray[0][1] = actionMaskArray[0][1]
    testArray[0][2] = actionMaskArray[0][2]
    testArray[0][3] = actionMaskArray[0][3]
    testArray[0][4] = actionMaskArray[0][4]
    feed_dict[actionMaskTensor] = testArray
    #feed_dict[actionMaskTensor] = np.zeros((1, 5), dtype=int)

    sess = tf.Session(graph=graph)

    return sess.run(fetchTensor, feed_dict=feed_dict)


print("done")